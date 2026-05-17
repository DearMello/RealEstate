# RealEstate API

Clean Architecture ilə yazılmış əmlak idarəetmə sistemi.

## Struktur

```
RealEstate/
├── src/
│   ├── RealEstate.Domain          # Entity, Enum, Interface
│   ├── RealEstate.Application     # Service, DTO, IService
│   ├── RealEstate.Infrastructure  # EF Core, Repository, JWT
│   └── RealEstate.API             # Controller, Program.cs
└── RealEstate.sln
```

## İşə salma

```bash
dotnet restore
cd src/RealEstate.API
dotnet ef migrations add InitialCreate --project ../RealEstate.Infrastructure
dotnet ef database update --project ../RealEstate.Infrastructure
dotnet run
```

Swagger: `https://localhost:{port}/swagger`

## API Endpoints

### Auth
- `POST /api/auth/register`
- `POST /api/auth/login`

### Properties
- `GET /api/properties`
- `GET /api/properties/{id}`
- `GET /api/properties/search?city=Baku&type=Apartment&minArea=50&maxArea=200` (anonim)
- `POST /api/properties`
- `PUT /api/properties/{id}`
- `DELETE /api/properties/{id}` (Admin)

### Agents
- `GET /api/agents`
- `GET /api/agents/{id}`
- `POST /api/agents` (Admin)
- `PUT /api/agents/{id}` (Admin)
- `DELETE /api/agents/{id}` (Admin)

### Clients
- `GET /api/clients`
- `GET /api/clients/{id}`
- `POST /api/clients`
- `PUT /api/clients/{id}`

### Listings
- `GET /api/listings`
- `GET /api/listings/{id}`
- `GET /api/listings/agent/{agentId}`
- `GET /api/listings/search?city=Baku&listingType=Sale&minPrice=50000&maxPrice=200000` (anonim)
- `POST /api/listings`
- `PUT /api/listings/{id}`
- `POST /api/listings/inquiries` (anonim - müştəri sorğu göndərir)
- `PUT /api/listings/inquiries/{id}/respond`
- `GET /api/listings/{id}/inquiries`

## Roles
- `Admin` — tam giriş
- `Agent` — ümumi əməliyyatlar
