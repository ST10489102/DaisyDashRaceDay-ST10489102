# DaisyDash Race Day - ST10489102

**Part 1 Submission | Race Event Management System**

### Overview
Manages race events, participants, and results with a structured database and API plan.

### Repo Structure
- `docs/DaisyDashRaceDay.sql` - Database with tables & data
- `docs/ERD.png` - Entity Relationship Diagram  
- `docs/API_Endpoint_Plan.md` - API plan (register, login, races, participants)
- `.github/workflows/ci.yml` - CI pipeline
- `SECTION B.pdf` - Documentation

### Database
Tables: Users, Races, Participants, Results. Includes PK/FK and sample data. ERD in docs/.

### API Endpoints (Planned)
POST /api/register, POST /api/login, GET /api/races, POST /api/races, GET /api/results

### CI/CD - GitHub Actions
Workflow validates all docs exist on every push. Status: ✅ Passing. View in Actions tab.

### Setup
1. Clone: git clone https://github.com/ST10489102/DaisyDashRaceDay-ST10489102.git
2. Import docs/DaisyDashRaceDay.sql to MySQL/SSMS

**Student:** ST10489102 | **Repo:** github.com/ST10489102/DaisyDashRaceDay-ST10489102
