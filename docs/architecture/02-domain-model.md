# Domain Model

This diagram describes the core business entities of Mini Jira.

The Domain layer contains the main business objects and their relationships.  
It does not depend on EF Core, ASP.NET Core, database configuration, DTOs, or API contracts.

```mermaid
erDiagram
    USER ||--o{ BOARD : owns
    BOARD ||--o{ COLUMN : contains
    COLUMN ||--o{ CARD : contains
    USER ||--o{ CARD : assigned_to

    USER {
        uuid id
        datetime createdAt
        datetime updatedAt
        string email
        string displayName
        string passwordHash
        string refreshToken
        datetime refreshTokenExpiresAt
    }

    BOARD {
        uuid id
        datetime createdAt
        datetime updatedAt
        string title
        string description
        uuid ownerId
    }

    COLUMN {
        uuid id
        datetime createdAt
        datetime updatedAt
        string title
        int order
        uuid boardId
    }

    CARD {
        uuid id
        datetime createdAt
        datetime updatedAt
        string title
        string description
        int order
        CardPriority priority
        datetime dueDate
        uuid columnId
        uuid assigneeId
    }
```

## Relationships

```txt
User 1 -> many Boards
Board 1 -> many Columns
Column 1 -> many Cards
User 1 -> many assigned Cards
```

## Shared Entity Fields

All domain entities inherit from `BaseEntity`.

```mermaid
classDiagram
    class BaseEntity {
        +Guid Id
        +DateTime CreatedAt
        +DateTime? UpdatedAt
        #SetUpdatedAt()
    }

    BaseEntity <|-- User
    BaseEntity <|-- Board
    BaseEntity <|-- Column
    BaseEntity <|-- Card
```

## Entity Responsibilities

```mermaid
classDiagram
    class User {
        +Email
        +DisplayName
        +PasswordHash
        +SetRefreshToken()
        +RevokeRefreshToken()
        +HasValidRefreshToken()
    }

    class Board {
        +Title
        +Description
        +OwnerId
        +Update()
        +AddColumn()
    }

    class Column {
        +Title
        +Order
        +BoardId
        +Update()
    }

    class Card {
        +Title
        +Description
        +Order
        +Priority
        +DueDate
        +ColumnId
        +AssigneeId
        +Move()
        +Update()
        +Assign()
    }
```