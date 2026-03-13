# Patch Management System

## Overview

The Patch Management System is designed to manage application patches and track deployments across different environments such as **Client, UAT, and Live (Production)**.

The system allows users to **upload, store, download, and maintain patch details**, ensuring proper tracking of where patches are deployed and maintaining deployment history for each client.

This project demonstrates practical experience in **application release management, deployment tracking, and database-driven application support**.

---

## Features

* Upload and store patch files
* Maintain patch details and version information
* Download patches when required
* Track where patches are deployed (Client / UAT / Live)
* Maintain deployment history
* Store patch metadata in the database
* Ensure organized patch management for multiple projects

---

## Technologies Used

* **Backend:** C# / .NET
* **Database:** SQL Server / MySQL
* **Frontend:** HTML, CSS, JavaScript
* **Version Control:** Git, GitHub

---

## System Workflow

1. User uploads a patch file to the system.
2. Patch details such as **patch name, version, project, environment, and deployment date** are stored in the database.
3. The system tracks where the patch is deployed:

   * Client Environment
   * UAT Environment
   * Live (Production) Environment
4. Users can download previously uploaded patches.
5. Deployment records help track the release history for each project.

---

## Example Data Stored

* Patch Name
* Project Name
* Version Number
* Deployment Environment (Client / UAT / Live)
* Deployment Date
* Patch File Location

---

## Future Improvements

* Role-based access control
* Automated deployment pipeline
* Integration with CI/CD tools (Jenkins)
* Deployment notifications and logs
* Patch approval workflow

---

## Author

Shreyas Chavare

GitHub: https://github.com/shreyas6578
LinkedIn: https://www.linkedin.com/in/shreyas-chavare-240908262/
