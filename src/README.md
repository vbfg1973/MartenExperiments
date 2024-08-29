# Marten Experiments

[Marten](https://martendb.io/) is a .NET document DB and Event Store that uses PostgreSQL as its backing database.

It is interesting because it can support CQRS operations in a low-infrastructure manner, using the event store for the write-side and in-built support for projections to populate fully hydrated read models on the read-side. It has internal support for the "Unit of Work" pattern, whereby an event is written to the event store and registered projections update read models as part of the same work unit. This vastly reduces the negative consequences of "eventual consistency" concerns. 

This project is an experiment with some of those features.

## Project Structure

### Core

Common features for this and any future projects

### Domain

Domain implementation

## Implementation philosophy

Basic premise: we use databases to keep track of the present state of things. The present state of things is the sum total of everything that has happened up to this point. 

A traditional database will only tell us the present state of things, and great effort must be made to understand that data over time. Periodic snapshots to data warehouses are a common approach, and can help us to understand trends. 

We can be more flexible in that temporal understanding by simply recording everything that ever happens. Business actions carried out by users and their effects become first-class data citizens, and under this scheme are the only things we record.

* the present state of things
* how the present state of things came to be
* how things were at specific moments in time

### CQRS

CQRS stands for *Command - Query Responsibility Segregation*. 

This is a mechanism for separating data that is optimised for reading, and data that is optimised for writing.

Data that is optimised for reading is an easy concept. Simply, how do we structure data so that it can be quickly and easily found, lifted off the disk and sent out to the client? 

Ideally, data that is optimised for writing will be fast too. But optimisation in the context of writing data is also about structuring so that its validity can be constantly assured.

### Key concepts - Write Side

#### Aggregate

An [aggregate](https://martinfowler.com/bliki/DDD_Aggregate.html) is a collection of data objects that pertain to a business problem. 

It is not really akin to a table in a rdbms, but closer to the collection of data necessary to solve a complete problem. Rather than an order, it is an order, the line items, the shipping address, the billing address, the customer, the costs, discounts applied for this customer in the context of this order, and any other data items necessary for our solution to the ordering problem all together as a complex object. 

In a traditional database we might only update a small part of an order, say the shipping address, and easily miss that the order has already shipped. By incorporating all data related to the problem in one object we can more easily and readily determine if operations are valid. Aggregates have an *AggregateRoot*, which is the entry point for all operations against the aggregate. The AggregateRoot has a uniquely identifying Id, and is likely the place where all business rules relevant to that aggregate are implemented. 

#### Command

Commands are the means by which those operations are carried out. Commands should have business-relevant names written in an imperative tense. *CreateOrder*, *UpdateShippingAddress*, etc. 

Although written in an imperative style, a command is a *request* that an action be carried out and not an imperative order that must be obeyed. It is the responsibility of the aggregate to determine, from its in-built business rules and present state, whether that request can be carried out.

The result of a command can be:

* To change state in the manner requested
* To throw an exception, indicating the command could not be carried out
* To fail silently, with no side-effects

#### Event

The event is the first-class citizen of this kind of system. The event is used to indicate that some aspect of state has changed, and carries enough information to describe what has changed. As such, events describe what has happened.

*ShippingAddressUpdated*, *OrderCreated*, *OrderShipped*

Under this philosophy, the only thing we store on the write side are events. Events represent things that have happened, and the present state of things is the history of all things that have happened up to this point.

Our aggregates are constructed so that they build themselves by replaying events. Events contain an id that relate them to the aggregate. They have a sequence number to indicate the order in which they occurred. The aggregate has overloaded *ApplyEvent* methods for each event that can occur, which alter the state of the aggregate according to the description of the state change on the event.

The process of replaying events to build an aggregate is commonly called *re-hydrating* the aggregate.

Marten has a number of features to support this work for us.

### Key Concepts - Read Side

#### Read Model

Read models are objects which support the quick and easy retrieval of data to the consuming client. It is possible to re-hydrate an aggregate and send it to the client, but read models offer a great deal more flexibility.

A good RDBMS analogy for a read model are views and materialised views. 

A view in a RDBMS is really a query whose results are a virtual table. That virtual table can itself be queried. This is a useful shorthand, but can hide expensive operations.

A materialised view is similar, except the results of the query that defines it are written to disk. When the materialised view is queried it is the results written to disk which are queried. This is likely to be much faster, but the data on the disk is only update periodically and can be out of sync with the real data in real tables.

The read model is similar to the materialised view in that it is a view that exists on the disk. The primary difference is that as events occur and are emitted by the aggregates, the read models receive those events and update themselves accordingly.   

The offer similar flexibility because multiple read models can receive the same events, allowing for different views to be created from the same source data.

#### Projection

The projection is the means of mapping the events to different read models. In some CQRS systems, a large amount of infrastructure is required to build projections, read models and the message buses that transport the events.

Marten has easy, in-built support for projections that eliminate the need for that infrastructure.
