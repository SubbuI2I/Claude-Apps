# DevPulse Specification

This specification documents the full DevPulse capstone project across five development phases.

## Phase 1: Requirements & Discovery

### Goal
Build a developer analytics dashboard that connects GitHub repositories, aggregates metrics, and displays insights through a secure full-stack application.

### Requirements
- User registration, login, JWT auth
- GitHub repository connection
- Metrics ingestion and visualization
- REST API with organized controllers
- PostgreSQL-backed persistence
- React frontend with charts and dashboard views
- CI/CD and security-focused project setup

### Success Criteria
- 5+ backend API endpoints
- 3+ related database tables
- 5+ frontend components/pages
- Automated tests with 80%+ coverage
- Full documentation and audit reporting

## Phase 2: Architecture & Design

### System Architecture
- Backend: .NET 7 Web API
- Database: PostgreSQL via Entity Framework Core
- Frontend: React + TypeScript + Vite
- Authentication: JWT tokens
- Deployment: Docker Compose + GitHub Actions

### Data Model
- `User` - authentication and account details
- `Repository` - connected GitHub repository metadata
- `Metric` - daily repository analytics
- `Session` - active user sessions and tokens
- `ActivityLog` - commit/PR/issue activity history

### API Design
- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/auth/validate`
- `GET /api/repositories`
- `POST /api/repositories/connect`
- `GET /api/repositories/{repoId}`
- `DELETE /api/repositories/{repoId}`
- `GET /api/metrics/dashboard`
- `GET /api/metrics/{repoId}`
- `GET /api/metrics/{repoId}/latest`
- `POST /api/metrics/{repoId}`

## Phase 3: Implementation

### Backend
- Implemented domain models and EF Core context
- Built services for authentication, repository management, metrics handling
- Exposed controllers for auth, repository, and metrics flows
- Added GitHub repository verification and JWT generation

### Frontend
- Created dashboard UI and login page
- Built reusable components: `RepositoryCard`, `MetricsChart`, `ProtectedRoute`
- Added data service layers for API integration
- Configured Vite, Tailwind, and secure environment handling

## Phase 4: Testing & CI/CD

### Automated Testing
- Added backend unit tests for service behavior
- Configured coverage collection with Coverlet
- Target coverage: 80%+

### CI/CD
- GitHub Actions workflow validates backend and frontend builds
- PostgreSQL service support for integration-style backend tests
- Docker Compose build step for main branch deployments

## Phase 5: Deployment & Delivery

### Deployment
- Docker Compose configuration for API, frontend, and database
- GitHub Actions pipeline for build/test automation
- Documentation for local setup and environment variables

### Delivery
- Documentation included in `Docs/`
- `README.md` updated with project setup and API overview
- Security audit added in `Docs/SECURITY-AUDIT.md`
- MCP integration via `.mcp.json`
- CLAUDE command definitions in `.claude/commands/`
