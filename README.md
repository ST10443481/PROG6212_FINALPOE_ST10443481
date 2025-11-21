# PROG6212_FINALPOE_ST10443481
GitHub Link: https://github.com/ST10443481/PROG6212_FINALPOE_ST10443481.git 
YouTube Link: https://youtu.be/_KoOj3A7fXs 

📘 Contract Monthly Claim System (CMCS)

A .NET Core MVC web application designed to streamline the monthly claim submission and approval process for Independent Contractor (IC) lecturers.
This project implements Part 3 (Automation) of the PROG6212 Portfolio of Evidence, including HR automation features, lecturer auto-calculation, admin workflow automation, reports, session management, and role-based access control.



⭐ 1. Overview

The Contract Monthly Claim System (CMCS) is a web-based system where:
Lecturers submit claims for hours worked.
Programme Coordinators and Academic Managers verify and approve claims.
HR manages users, rates, and can generate reports.
This version uses in-memory storage instead of a database, as per student requirements.

🚀 2. Features

✔ Lecturer Features

Login system using Sessions
Auto-filled personal details (Name, Hourly Rate)
Auto-calculation of claim amount
Hours validation (maximum 180 hours)
Upload supporting documents
Track claim status ("Pending", "Approved", "Rejected")

✔ HR Features

Add new users (Lecturers, Coordinators, Managers)
Update existing users
Manage hourly rates
Generate reports (LINQ-based)
Acts as a super user
No registration — HR creates all profiles manually

✔ Admin Features

Two separate admin views:

Programme Coordinator

Academic Manager

Admins can:

View pending claims
Approve or reject claims
Access restricted using session roles
Cannot access pages belonging to opposite role

✔ Automation Features (Part 3)

Auto-calculation (Hours × Rate)
Auto-filled hourly rate
Automated validation
Workflow automation: Submit → Verify → Approve
Role-based access
Session-based login control
LINQ reporting

🏗 3. Architecture
Technologies

ASP.NET Core MVC
Razor Views
C#
Session-Based Authentication
LINQ for processing reports

Project Structure
/Controllers
/Models
/Views
/Services
/DataStore (In-Memory Lists)
wwwroot


🖥 5. Usage Guide

Lecturer Login

HR assigns a lecturer name and ID.
Lecturer logs in via Auth/Login.
Session stores LecturerId.
Submitting a Claim
Go to Claims → New Claim
Enter hours worked
Upload document
Auto-calculation displays total
Submit
Admin Workflow
Coordinator verifies claims
Academic Manager approves claims
Status updates in real time
HR Management
Add/update users
Create reports

🔧 6. Part 3 Enhancements (Fully Implemented)

Requirement	Status
HR as super user	✔ Completed
HR adds & updates all users	✔ Completed
Lecturer auto-rate from HR	✔ Implemented
Auto-calculation (hours × rate)	✔ Implemented
Validation (180-hour limit)	✔ Implemented
No registration — HR creates users	✔ Implemented
Session management	✔ Implemented
Role protection	✔ Implemented
Two separate admin views	✔ Implemented
Report generation	✔ Implemented
ReadMe updated with changes	✔ Completed
Minimum 10 Git commits	✔ Completed

📝 7. Lecturer Feedback & System Improvements

Feedback received (Part 2):
Needed auto calculation → Added
Remove manual rate entry → Rate now auto-populated
Add session management → Implemented
Add role protection → Implemented
Claims must be trackable → Completed

🔄 8. Version Control

A minimum of 10 detailed commits were created for Part 3, with descriptive messages such as:
Added HR automation and in-memory user management
Implemented auto calc for lecturer claims
Added session/role protection for admin views
Integrated claim workflow automation

🛠 9. Technologies Used

ASP.NET Core MVC
C#
HTML, CSS, Js
Sessions
LINQ
PowerPoint (for presentation)


Student Number: Thando Futwa - ST10443481
Module: PROG6212 
