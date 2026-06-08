# DevPulse — Developer Analytics Dashboard

A full-stack developer analytics dashboard that connects to GitHub repositories and shows real-time commit frequency, PR stats, and team activity metrics.

## 🎯 Features

- **Real-time Metrics**: Commit frequency, PR stats, issue tracking
- **GitHub Integration**: Connect and sync GitHub repositories
- **Team Analytics**: View aggregated metrics across multiple repos
- **Authentication**: Secure JWT-based authentication
- **Time-range Filtering**: View metrics for specific date ranges
- **Responsive Dashboard**: Modern UI with charts and analytics

## 🏗️ Architecture

```
DevPulse/
├── Backend/
│   ├── DevPulse.API (REST API, Controllers)
│   ├── DevPulse.Data (Entity Framework, Database models)
│   └── DevPulse.Core (Business logic, Services)
├── Frontend/
│   ├── src/
│   │   ├── components/ (React components)
│   │   ├── pages/ (Dashboard, Login pages)
│   │   ├── services/ (API client services)
│   │   ├── hooks/ (Custom React hooks)
│   │   └── types/ (TypeScript interfaces)
│   └── public/
└── Docs/
```

## 🚀 Getting Started

### Prerequisites

- **.NET 8.0+**
- **Node.js 18+**
- **PostgreSQL 13+**
- **Git**

### Backend Setup

1. Navigate to backend directory:
   ```bash
   cd Backend/DevPulse.API
   ```

2. Install dependencies:
   ```bash
   dotnet restore
   ```

3. Update `appsettings.json` with your database connection string:
   ```json
   {
     "ConnectionStrings": {
       "PostgresConnection": "Host=localhost;Port=5432;Database=devpulse;Username=postgres;Password=postgres"
     },
     "GitHub": {
       "Token": "your-github-personal-access-token"
     }
   }
   ```

4. Create and migrate database:
   ```bash
   dotnet ef database update
   ```

5. Run the API:
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:7294`

### Frontend Setup

1. Navigate to frontend directory:
   ```bash
   cd Frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start development server:
   ```bash
   npm run dev
   ```

The frontend will be available at `http://localhost:3000`

## 📡 API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login user
- `GET /api/auth/validate` - Validate token

### Repositories
- `GET /api/repositories` - List connected repositories
- `POST /api/repositories/connect` - Connect GitHub repository
- `GET /api/repositories/{repoId}` - Get repository details
- `DELETE /api/repositories/{repoId}` - Disconnect repository

### Metrics
- `GET /api/metrics/dashboard` - Get aggregated dashboard metrics
- `GET /api/metrics/{repoId}` - Get repository metrics (with date range)
- `GET /api/metrics/{repoId}/latest` - Get latest metrics
- `POST /api/metrics/{repoId}` - Record new metrics

## 🔑 Environment Variables

### Backend (.env or appsettings.json)
```
PostgresConnection=Host=localhost;Port=5432;Database=devpulse;Username=postgres;Password=postgres
GitHub:Token=your-github-personal-access-token
GitHub:ClientId=your-client-id
GitHub:ClientSecret=your-client-secret
JwtSettings:Secret=your-jwt-secret-key
JwtSettings:ExpirationMinutes=60
```

### Frontend (.env.development)
```
VITE_API_URL=https://localhost:7294
```

## 📊 Database Schema

### Core Tables
- **Users** - User accounts and authentication
- **Repositories** - Connected GitHub repositories
- **Metrics** - Time-series metrics data
- **Sessions** - User session tokens
- **ActivityLogs** - Recent commits, PRs, issues

## 🔧 Development

### Build Backend
```bash
cd Backend/DevPulse.API
dotnet build
```

### Build Frontend
```bash
cd Frontend
npm run build
```

### Run Tests
```bash
dotnet test
```

## 🐳 Docker Support

Build and run with Docker Compose:
```bash
docker-compose up -d
```

## 📝 Database Migrations

Add a new migration:
```bash
cd Backend/DevPulse.API
dotnet ef migrations add MigrationName --project ../DevPulse.Data
```

Update database:
```bash
dotnet ef database update --project ../DevPulse.Data
```

## 🔐 Security

- JWT-based authentication
- Password hashing with BCrypt
- CORS policy for frontend integration
- Secure token validation

## 📚 Tech Stack

### Backend
- **.NET 8** - Web framework
- **Entity Framework Core** - ORM
- **PostgreSQL** - Database
- **Octokit** - GitHub API client
- **JWT** - Authentication

### Frontend
- **React 18** - UI library
- **TypeScript** - Type safety
- **Vite** - Build tool
- **React Router** - Navigation
- **Recharts** - Data visualization
- **Tailwind CSS** - Styling
- **Axios** - HTTP client

## 🚧 Future Enhancements

- [ ] Real-time data sync with GitHub webhooks
- [ ] Advanced analytics and trend analysis
- [ ] Team collaboration features
- [ ] Custom dashboards
- [ ] Export metrics to CSV/PDF
- [ ] Slack integration
- [ ] Email notifications
- [ ] Performance optimization
- [ ] Dark mode

## 📄 License

MIT License - feel free to use this project for personal or commercial purposes.

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📧 Support

For issues or questions, please open an issue on GitHub.
