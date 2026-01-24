# Mermaid Diagram Examples

Mermaid lets you create diagrams using simple text syntax! Here are some examples relevant to Clarion development:

## 1. Database Entity Relationship Diagram

Perfect for documenting your Clarion FILE structures:

```mermaid
erDiagram
    CUSTOMER ||--o{ ORDER : places
    CUSTOMER {
        long CUS_ID PK
        string CUS_Name
        string CUS_Email
        string CUS_Phone
    }
    ORDER ||--|{ LINE_ITEM : contains
    ORDER {
        long ORD_ID PK
        long ORD_CustomerID FK
        date ORD_Date
        decimal ORD_Total
    }
    PRODUCT ||--o{ LINE_ITEM : "ordered in"
    PRODUCT {
        long PRO_ID PK
        string PRO_Name
        decimal PRO_Price
        long PRO_StockQty
    }
    LINE_ITEM {
        long LIN_ID PK
        long LIN_OrderID FK
        long LIN_ProductID FK
        long LIN_Quantity
        decimal LIN_UnitPrice
    }
```

## 2. Program Flow / Sequence Diagram

Document how your Clarion procedures interact:

```mermaid
sequenceDiagram
    participant User
    participant LoginProc
    participant ValidateProc
    participant Database
    participant MainMenu
    
    User->>LoginProc: Enter credentials
    LoginProc->>ValidateProc: Check username/password
    ValidateProc->>Database: SELECT from USERS
    Database-->>ValidateProc: Return user record
    
    alt Valid credentials
        ValidateProc-->>LoginProc: Success
        LoginProc-->>User: Welcome message
        LoginProc->>MainMenu: Open()
        MainMenu-->>User: Display menu
    else Invalid credentials
        ValidateProc-->>LoginProc: Failure
        LoginProc-->>User: Error message
        LoginProc->>LoginProc: Clear fields
    end
```

## 3. Flowchart - Decision Logic

Document complex IF/CASE structures:

```mermaid
flowchart TD
    Start([User clicks Save]) --> Validate{Validate Form}
    Validate -->|Invalid| ShowError[Display Error Message]
    ShowError --> End1([Return])
    
    Validate -->|Valid| CheckMode{New or Edit?}
    CheckMode -->|New Record| AddRecord[ADD File Record]
    CheckMode -->|Edit Record| UpdateRecord[PUT File Record]
    
    AddRecord --> CheckError{IO Error?}
    UpdateRecord --> CheckError
    
    CheckError -->|Error| ShowIOError[Display IO Error]
    ShowIOError --> End2([Return])
    
    CheckError -->|Success| Commit[COMMIT]
    Commit --> ShowSuccess[Display Success Message]
    ShowSuccess --> CloseWindow[Close Window]
    CloseWindow --> End3([Return])
```

## 4. State Diagram - Screen/Window States

Document window behavior:

```mermaid
stateDiagram-v2
    [*] --> Idle
    Idle --> Browsing : User selects record
    Browsing --> Editing : User clicks Edit
    Browsing --> Inserting : User clicks Add
    
    Editing --> Saving : User clicks Save
    Inserting --> Saving : User clicks Save
    
    Saving --> Browsing : Save successful
    Saving --> Editing : Save failed
    Saving --> Inserting : Save failed
    
    Editing --> Browsing : User clicks Cancel
    Inserting --> Browsing : User clicks Cancel
    
    Browsing --> [*] : User closes window
```

## 5. Gantt Chart - Project Timeline

Plan your development sprints:

```mermaid
gantt
    title Clarion Project Development Schedule
    dateFormat YYYY-MM-DD
    section Phase 1
    Database Design      :done,    des1, 2026-01-01, 2026-01-10
    File Structure Setup :done,    des2, 2026-01-11, 2026-01-15
    section Phase 2
    Login Screen         :active,  dev1, 2026-01-16, 2026-01-20
    Main Menu            :         dev2, 2026-01-21, 2026-01-25
    section Phase 3
    Customer Management  :         dev3, 2026-01-26, 2026-02-05
    Order Processing     :         dev4, 2026-02-06, 2026-02-15
    section Testing
    Unit Testing         :         test1, 2026-02-16, 2026-02-20
    Integration Testing  :         test2, 2026-02-21, 2026-02-25
```

## 6. Class Diagram - OOP Structure

For ABC/Clarion OOP designs:

```mermaid
classDiagram
    class FileManager {
        +FILE FileHandle
        +STRING FileName
        +LONG ErrorCode
        +Open() BOOL
        +Close()
        +GetError() STRING
    }
    
    class CustomerManager {
        +QUEUE CustomerQ
        +LoadCustomers()
        +SaveCustomer()
        +DeleteCustomer()
        +FindByEmail() BOOL
    }
    
    class OrderManager {
        +QUEUE OrderQ
        +CreateOrder()
        +UpdateOrderStatus()
        +CalculateTotal() DECIMAL
    }
    
    FileManager <|-- CustomerManager
    FileManager <|-- OrderManager
    
    CustomerManager "1" --> "*" OrderManager : has
```

## 7. Git Flow - Branching Strategy

Document your repository workflow:

```mermaid
gitgraph
    commit id: "Initial commit"
    commit id: "Add login module"
    branch develop
    checkout develop
    commit id: "Start customer module"
    branch feature/customer-crud
    checkout feature/customer-crud
    commit id: "Add customer form"
    commit id: "Add validation"
    checkout develop
    merge feature/customer-crud
    checkout main
    merge develop tag: "v1.0"
    checkout develop
    commit id: "Bug fixes"
```

## Tips for Using Mermaid in Clarion Projects

- **Document as you code**: Add mermaid diagrams to README files in each module
- **ERD diagrams**: Perfect for the .DCT dictionary documentation
- **Flowcharts**: Great for documenting complex LOOP/IF structures before coding
- **Sequence diagrams**: Show multi-procedure interactions (especially with global extensions)
- **Version control**: Since it's text, diagrams are diffable in git!

---

*Edit this file in the Markdown Editor and see the diagrams render in real-time!* 🎉
