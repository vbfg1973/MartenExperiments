# Marten Experiments

[Marten](https://martendb.io/) is a .NET document DB and Event Store that uses PostgreSQL as its backing database.

It is interesting because it can support CQRS operations in a low-infrastructure manner, using the event store for the write-side and in-built support for projections to populate fully hydrated read models on the read-side. It has internal support for the "Unit of Work" pattern, whereby an event is written to the event store and registered projections update read models as part of the same work unit. This vastly reduces the negative consequences of "eventual consistency" concerns. 

This project is an experiment with some of those features.

## Project Structure

### Core

Common features for this and any future projects

### Domain
