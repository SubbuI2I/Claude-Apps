# Contributing to DevPulse

Thank you for your interest in contributing! Here's how you can help:

## Development Setup

1. Fork the repository
2. Clone your fork: `git clone https://github.com/yourusername/DevPulse.git`
3. Create a branch: `git checkout -b feature/your-feature`
4. Follow the setup instructions in [SETUP.md](SETUP.md)

## Code Style

### C# (Backend)
- Follow Microsoft C# coding conventions
- Use PascalCase for public members
- Use private backing fields for properties
- 4-space indentation
- Use `var` when type is obvious
- XML documentation comments for public APIs

```csharp
/// <summary>
/// Connects a GitHub repository to the user's account
/// </summary>
public async Task<Repository> ConnectRepositoryAsync(string owner, string name)
{
    // Implementation
}
```

### TypeScript/React (Frontend)
- Use PascalCase for component names
- Use camelCase for functions and variables
- 2-space indentation
- Use functional components with hooks
- Props interfaces ending with `Props`

```typescript
interface DashboardProps {
  userId: string;
  onUpdate?: () => void;
}

export const Dashboard: React.FC<DashboardProps> = ({ userId, onUpdate }) => {
  // Implementation
};
```

## Commit Messages

Follow Conventional Commits format:

```
type(scope): subject

body

footer
```

Types: `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`

Examples:
- `feat(auth): add JWT token refresh mechanism`
- `fix(metrics): correct time range filtering`
- `docs: update API endpoint documentation`

## Pull Request Process

1. Update your branch with latest `main`: `git fetch origin && git rebase origin/main`
2. Run tests: `dotnet test` (backend), `npm run lint` (frontend)
3. Build the project: `dotnet build`, `npm run build`
4. Write clear PR description with:
   - What does this PR do?
   - Why is this change needed?
   - Screenshots/recordings if UI changes
   - Checklist of testing performed

## Testing

### Backend
```bash
cd Backend/DevPulse.API
dotnet test
```

### Frontend
```bash
cd Frontend
npm test
```

## Running Locally

### With dotnet and npm
```bash
# Terminal 1: Backend
cd Backend/DevPulse.API
dotnet run

# Terminal 2: Frontend
cd Frontend
npm run dev
```

### With Docker
```bash
docker-compose up -d
```

## Documentation

- Update [README.md](README.md) for user-facing changes
- Update [ARCHITECTURE.md](ARCHITECTURE.md) for structural changes
- Update [SETUP.md](SETUP.md) for setup/installation changes
- Add inline code comments for complex logic

## Git Workflow

```
main (production-ready)
  └── develop (integration branch)
      └── feature/my-feature (your work)
```

1. Create feature branch from `develop`
2. Create PR to `develop`
3. After review and tests pass, merge to `develop`
4. Periodically merge `develop` to `main` for releases

## Code Review Checklist

- [ ] Code follows style guidelines
- [ ] Changes are tested
- [ ] Documentation is updated
- [ ] No debug code or console.logs left
- [ ] Performance impact considered
- [ ] Security implications reviewed

## Reporting Issues

Use GitHub Issues with:
- Clear title
- Detailed description
- Steps to reproduce
- Expected vs actual behavior
- Environment details

## Questions?

- Open an issue with the `question` label
- Check existing documentation first
- Review closed issues for similar topics

Thank you for contributing to DevPulse! 🚀
