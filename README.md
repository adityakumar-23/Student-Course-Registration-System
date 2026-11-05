# Student Course Registration System
### Marwadi University &bull; Self-Learning Activity (ASP.NET Core MVC)

A modern, responsive, and robust **Student Course Registration System** developed using **ASP.NET Core MVC (C#)**, **Bootstrap 5**, and **Razor Views**, adhering strictly to the MVC architecture, Model Binding, and Data Annotation validation specifications.

---

## 🌟 Key Features

1. **Interactive Dashboard (Home Page)**:
   - Dynamic real-time statistics (Total Enrolled Students, Available Courses, Active Semesters, System Status).
   - Recent registrations table.
   - Course enrollment distribution breakdown.
   - Technical highlights for quick revision during viva.

2. **Student Course Registration (`Student/Create`)**:
   - Modern registration card with floating icons and clear placeholders.
   - Academic program / course selection dropdown with pre-configured university courses.
   - Semester dropdown (1 to 8).
   - Data Annotation validation with instant client-side feedback and robust server-side enforcement.
   - Duplicate Student ID detection.

3. **Student Directory (`Student/Index`)**:
   - Clean Bootstrap 5 table displaying all registered students.
   - Search bar filtering by Student ID, Student Name, or Email.
   - Filter dropdown by Academic Program/Course.
   - Color-coded badges for Course, Semester, and Enrollment Status.
   - Actions to inspect complete student details or remove records with a confirmation dialog.

4. **Student Academic Profile (`Student/Details`)**:
   - Comprehensive profile view displaying all registered academic fields.
   - Auto-generated student avatar initials.
   - Direct navigation back to the directory or to register another student.

5. **In-Memory Thread-Safe Data Store**:
   - Built using a static `List<Student>` repository (`StudentRepository.cs`).
   - Pre-seeded with realistic sample student records so the dashboard and tables are immediately populated upon launch.

---

## 🏗️ MVC Architecture & Code Structure

```
StudentCourseRegistration/
├── Controllers/
│   ├── HomeController.cs           # Manages the Dashboard & statistical aggregations
│   └── StudentController.cs        # Handles Listing, Registration, Details, and Deletion
├── Models/
│   ├── Student.cs                  # Student entity with Data Annotations validation
│   ├── StudentRepository.cs        # Static in-memory data store with CRUD operations
│   ├── DashboardViewModel.cs       # Aggregated metric container for the dashboard
│   └── ErrorViewModel.cs           # Standard MVC error container
├── Views/
│   ├── Home/
│   │   └── Index.cshtml            # Dashboard view with KPI cards & recent registrations
│   ├── Student/
│   │   ├── Index.cshtml            # Student directory with search, filter & Bootstrap table
│   │   ├── Create.cshtml           # Course registration form with validation
│   │   └── Details.cshtml          # Individual student profile view
│   └── Shared/
│       ├── _Layout.cshtml          # Master layout with responsive navbar & toast alerts
│       ├── _ValidationScriptsPartial.cshtml # jQuery Unobtrusive validation bundle
│       ├── _ViewImports.cshtml     # Shared namespaces & tag helpers
│       └── _ViewStart.cshtml       # Default layout pointer
├── wwwroot/
│   ├── css/
│   │   └── site.css                # Custom styling, university palette, gradients & cards
│   └── lib/                        # Bootstrap 5 & jQuery libraries
├── Program.cs                      # Application pipeline, routing & dependency injection
└── StudentCourseRegistration.csproj
```

---

## 🚀 How to Run the Application

### Option A: Using the Terminal / Command Prompt / PowerShell

1. Open a terminal and navigate to the project directory:
   ```bash
   cd c:\Users\adity\OneDrive\Desktop\StudentCourseRegistration
   ```
2. Run the application:
   ```bash
   dotnet run
   ```
3. Open your browser and navigate to the displayed URL (typically `http://localhost:5237` or `https://localhost:7129`).

---

### Option B: Using Visual Studio 2022 / 2025

1. Open Visual Studio.
2. Click **Open a project or solution** and select `StudentCourseRegistration.csproj`.
3. Press **F5** (or click the green **Run / Start Debugging** button with the IIS Express / `StudentCourseRegistration` profile).
4. The default web browser will launch automatically showing the Dashboard.

---

### Option C: Using Visual Studio Code

1. Open the project folder in VS Code (`File` -> `Open Folder...`).
2. Open the integrated terminal (`Ctrl + ~`).
3. Execute:
   ```bash
   dotnet run
   ```
4. Click or browse to the local URL provided in the terminal output.

---

## 🎓 University Viva Voce Preparation Guide

### 1. What is the MVC pattern and how is it used in this project?
- **Model** (`Models/Student.cs`): Represents the data structure, rules, and business logic. It defines what attributes a student possesses and the validation constraints.
- **View** (`Views/Student/*.cshtml`): The user interface rendered using Razor syntax and HTML5/Bootstrap 5. It presents data to the user and captures user input.
- **Controller** (`Controllers/StudentController.cs`): The intermediary that receives incoming HTTP requests, interacts with the Model/Repository to fetch or persist data, and selects the appropriate View to render.

### 2. What are Data Annotations and which ones did you use?
Data Annotations are declarative attributes applied to model properties in the `System.ComponentModel.DataAnnotations` namespace.
- `[Required]`: Ensures the user cannot submit an empty field.
- `[StringLength(50, MinimumLength = 3)]`: Enforces string length boundaries (e.g., student name between 3 and 50 characters).
- `[Range(1000, 999999)]`: Validates that numeric inputs fall within an acceptable range (e.g., Student ID between 1000 and 999999; Semester between 1 and 8).
- `[EmailAddress]`: Uses regex pattern matching to ensure valid email formatting.
- `[Display(Name = "...")]`: Sets human-readable labels rendered by Razor Tag Helpers like `<label asp-for="StudentName">`.

### 3. What is Model Binding in ASP.NET Core?
Model Binding is the automatic process where ASP.NET Core extracts incoming data from HTTP request sources (Form collections, Route data, Query strings) and converts them into strongly typed C# objects passed as parameters to controller actions.
- *Example in this project*: When the user submits the registration form, the values from `<input asp-for="StudentName" />`, `<input asp-for="Email" />`, etc. are automatically mapped to the properties of `Student student` in `public IActionResult Create(Student student)`.

### 4. What is `ModelState.IsValid` and why is it important?
`ModelState` is a dictionary storing the state of model binding and validation.
- `ModelState.IsValid` checks whether any validation errors occurred during model binding or Data Annotation checks.
- If `ModelState.IsValid` is `true`, data adheres to all rules and is safe to save in `StudentRepository.Add(student)`.
- If `false`, the controller re-renders `return View(student)`, allowing Razor to display specific error messages next to invalid fields via `<span asp-validation-for="...">`.

### 5. How does Client-Side vs. Server-Side Validation work here?
- **Client-Side**: Executed in the user's browser via jQuery Validation and jQuery Unobtrusive Validation (`_ValidationScriptsPartial.cshtml`). It provides instant feedback without waiting for a server round-trip.
- **Server-Side**: Executed on the web server inside `StudentController.Create()`. Even if JavaScript is disabled or bypassed by an attacker, `ModelState.IsValid` ensures invalid or duplicate data cannot be recorded.

### 6. How is data persisted without a database?
An in-memory static collection (`private static readonly List<Student> _students`) in `StudentRepository.cs` holds all records in application memory across HTTP requests, pre-seeded with 5 realistic student records.

