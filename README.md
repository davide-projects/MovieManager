# MovieManager

Gestione di film, attori, registi e recensioni con architettura a più livelli (.NET 10).

## Architettura

```
MovieManager.slnx
 ├── MovieManager.DAL          – Data Access Layer (EF Core, SQL Server)
 ├── MovieManager.BLL          – Business Logic Layer (AutoMapper, servizi generici)
 ├── MovieManager.PL.API       – REST API (Scalar/OpenAPI)
 └── MovieManager.PL.MVC       – Web App MVC (Bootstrap, jQuery)
```

### DAL
- Entity Framework Core 10.0.9 con SQL Server
- Pattern **Repository** generico + **Unit of Work**
- Repository dedicato `MovieActorRepository` per chiave composta

### BLL
- Servizio generico `GenericService<TEntity, TModel>` con vincolo `IModelWithId`
- `MovieActorService` dedicato per entità a chiave composta (`MovieId`, `ActorId`)
- Mapping Entity ↔ Model con **AutoMapper 14**

### PL.API
- Endpoint RESTful con documentazione OpenAPI tramite **Scalar**
- Risposte con messaggi descrittivi in inglese (es. `"Actor with id {id} not found."`)
- Dipende da BLL per la logica applicativa

### PL.MVC
- Interfaccia utente con ASP.NET Core MVC
- Bootstrap 5, jQuery, jQuery Validation

## Tecnologie

| Componente | Versione |
|---|---|
| .NET | 10.0 |
| Entity Framework Core | 10.0.9 |
| AutoMapper | 14.0.0 |
| Scalar | 2.16.11 |
| Microsoft.AspNetCore.OpenApi | 10.0.9 |
| SQL Server | — |

## Endpoint API

| Metodo | Route | Descrizione |
|---|---|---|
| GET | `/api/actors` | Elenco attori |
| GET | `/api/actors/{id}` | Attore per id |
| POST | `/api/actors` | Crea attore |
| PUT | `/api/actors/{id}` | Aggiorna attore |
| DELETE | `/api/actors/{id}` | Elimina attore |
| GET | `/api/directors` | Elenco registi |
| GET | `/api/directors/{id}` | Regista per id |
| POST | `/api/directors` | Crea regista |
| PUT | `/api/directors/{id}` | Aggiorna regista |
| DELETE | `/api/directors/{id}` | Elimina regista |
| GET | `/api/genres` | Elenco generi |
| GET | `/api/genres/{id}` | Genere per id |
| POST | `/api/genres` | Crea genere |
| PUT | `/api/genres/{id}` | Aggiorna genere |
| DELETE | `/api/genres/{id}` | Elimina genere |
| GET | `/api/movies` | Elenco film |
| GET | `/api/movies/{id}` | Film per id |
| POST | `/api/movies` | Crea film |
| PUT | `/api/movies/{id}` | Aggiorna film |
| DELETE | `/api/movies/{id}` | Elimina film |
| GET | `/api/reviews` | Elenco recensioni |
| GET | `/api/reviews/{id}` | Recensione per id |
| POST | `/api/reviews` | Crea recensione |
| PUT | `/api/reviews/{id}` | Aggiorna recensione |
| DELETE | `/api/reviews/{id}` | Elimina recensione |
| GET | `/api/movieactors/movie/{movieId}` | Ruoli per film |
| GET | `/api/movieactors/{movieId}/{actorId}` | Ruolo per film e attore |
| POST | `/api/movieactors` | Crea associazione |
| PUT | `/api/movieactors/{movieId}/{actorId}` | Aggiorna associazione |
| DELETE | `/api/movieactors/{movieId}/{actorId}` | Elimina associazione |

## Prerequisiti

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server (locale o remoto)

## Configurazione

Modificare la stringa di connessione in `MovieManager.PL.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "MovieDBString": "Server=localhost;Database=MovieManager;User Id=sa;Password=root"
  }
}
```

## Esecuzione

```bash
# API (disponibile su https://localhost:5001, documentazione Scalar su /scalar/v1)
dotnet run --project MovieManager.PL.API

# MVC
dotnet run --project MovieManager.PL.MVC
```

## Licenza

MIT © 2026 Davide Barbieri
