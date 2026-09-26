# 📦 View_Component_AggregateI_Mvc_Code_First

> A modular ASP.NET Core MVC project using Entity Framework Core (Code First) to demonstrate View Components and Aggregated UI logic.

---

## 📚 About This Project

This ASP.NET Core MVC web application is developed as a learning tool to explore:

- **Entity Framework Core Code First**: Database schema generated from C# classes.
- **View Components**: Reusable UI blocks like mini-controllers.
- **MVC Architecture**: Clean separation of logic (Model), routing (Controller), and design (View).
- **AppSettings Injection**: Strongly-typed configuration using DI.

It’s ideal for students, beginners, or intermediate developers who want a clean start with .NET Core MVC and Razor Views.

---

## 🏗 Project Tech Stack

| Technology        | Purpose                        |
|-------------------|---------------------------------|
| ASP.NET Core MVC  | Web framework                   |
| EF Core           | Data access (Code First)        |
| SQL Server (LocalDB) | Lightweight local database     |
| Razor View Engine | UI rendering engine             |
| Visual Studio 2022| Development environment         |

---

## ⚙ Configuration & Setup

### 📄 appsettings.json

```json
"ConnectionStrings": {
  "con": "server=(localdb)\MSSQLLocalDB; database=CoreEvdDBSonia4; Trusted_Connection=true; TrustServerCertificate=true"
}
```

### ⏯ Run Locally

```bash
git clone https://github.com/Sonia66Uub/View_Component_AggregateI_Mvc_Code_First.git
cd WebApplication1
dotnet restore
dotnet ef database update
dotnet run
```

Make sure to install EF tools:
```bash
dotnet tool install --global dotnet-ef
```

---

## 📁 Project Structure

```
├── Controllers/         --> MVC Controllers
├── Models/              --> EF Core Models
├── Views/               --> Razor Views
│   ├── Shared/          --> Shared layout & components
├── wwwroot/             --> Static files (css, js)
├── appsettings.json     --> Main config file
├── Program.cs           --> App startup
└── WebApplication1.csproj
```

---

## 🔄 Features

- 🧱 EF Core Code First Migration Ready
- 🧩 View Component Demo Ready
- 🧭 Conventional Routing: `/Home/Index/{id?}`
- 🗃 Logging configuration per environment
- 🔌 Dependency Injection configured for `AppDbContext`

--_

---

## 🧪 Dev Notes

To create a new migration:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 🤝 Contributing

Pull requests welcome! For major changes, please open an issue first to discuss.

---

## 📄 License

MIT License - use freely for educational or professional use.

🎥 Demo Video

🔹 Watch Full Walkthrough on YouTube
👉 https://youtu.be/4XlwvRq-h8o

---
🤝 Contribution Guide

Fork the repository

Create a new branch (feature/your-feature)

Commit your changes

Push and open a Pull Request 🎉

📜 License

This project is licensed under the MIT License – feel free to use, modify, and enhance!

📫 Contact
<p align="center"> Developed with ❤️ by <strong>SONIA KHATUN</strong><br/> 📧 <a href="mailto:yesminsonia66@gmail.com">yesminsonia66@gmail.com</a><br/> 🌐 <a href="https://github.com/Sonia66Hub" target="_blank">GitHub Profile</a> </p> ```
