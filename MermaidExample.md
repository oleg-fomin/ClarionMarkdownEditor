# Mermaid Diagram Examples

This markdown editor now supports **Mermaid diagrams**! Just use` ```mermaid ` code blocks.

## Example 1: Colorful Flowchart

```mermaid
graph TD
    A[Start]:::startClass --> B{Is it working?}
    B -->|Yes| C[🎉 Success!]:::successClass
    B -->|No| D[Debug it]:::debugClass
    D --> A
    
    classDef startClass fill:#4a90e2,stroke:#2e5c8a,color:#fff
    classDef successClass fill:#82ca9d,stroke:#5a9d6f,color:#fff
    classDef debugClass fill:#ffc658,stroke:#d9a13a,color:#000
```

## Example 2: Sequence Diagram

```mermaid
sequenceDiagram
    participant User
    participant Editor
    participant WebView2
    
    User->>Editor: Type markdown
    Editor->>WebView2: Update preview
    WebView2->>WebView2: Render Mermaid
    WebView2-->>User: Show diagram
```

## Example 3: Entity Relationship Diagram

```mermaid
erDiagram
    CUSTOMER ||--o{ ORDER : places
    ORDER ||--|{ LINE_ITEM : contains
    CUSTOMER {
        string name
        string email
    }
    ORDER {
        int orderNumber
        date orderDate
    }
```

## Example 4: Gantt Chart

```mermaid
gantt
    title Project Timeline
    dateFormat  YYYY-MM-DD
    section Phase 1
    Design           :done, a1, 2026-01-01, 10d
    Development      :active, a2, after a1, 15d
    section Phase 2
    Testing          :a3, after a2, 5d
    Deployment       :crit, a4, after a3, 2d
```

## Example 5: State Diagram

```mermaid
stateDiagram-v2
    [*] --> Editing
    Editing --> Saving : User clicks Save
    Saving --> Editing : Save failed
    Saving --> Browsing : Save successful
    Browsing --> Editing : User clicks Edit
    Browsing --> [*] : User closes
```

## Regular Code Blocks Still Work!

```clarion
MyProcedure PROCEDURE()
Message CODE
  MESSAGE('Hello from Clarion!')
  RETURN
```

```javascript
function hello() {
    console.log('JavaScript works too!');
}
```

Pretty cool, right? 🚀
