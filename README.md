# SeinfeldAPI 🎭

A custom-built ASP.NET Core Web API that brings the iconic world of *Seinfeld* to life through structured episode and quote data. Fully layered, RESTful, and now proudly hosted live on the internet.

<ins>URLs</ins>  
[Episodes](https://api.mortensens.xyz/seinfeld/api/episodes)  
[Episode Quotes](https://api.mortensens.xyz/seinfeld/api/episodequotes)

---

## 📦 Overview

The SeinfeldAPI is a RESTful service that allows users to:

- 📺 View all episodes
- 💬 Fetch quotes per episode
- ➕ Add new episodes and quotes
- ✏️ Update existing entries
- 🗑️ Delete episodes and quotes

Built with clarity and control in mind, using a layered architecture (Models → Repositories → Services → Controllers).

---

## 🧱 Tech Stack

- **Backend:** ASP.NET Core 6
- **Database:** SQL Server
- **Hosting:** Custom NGINX reverse proxy with Cloudflare Tunnel
- **Architecture:** Layered (DTOs, Services, Repositories, EF Model)

---

## 📁 Project Structure

SeinfeldAPI/  
├── Controllers/  
│ └── EpisodeController.cs  
│ └── EpisodeQuotesController.cs  
├── Models/  
│ ├── Episode.cs  
│ └── EpisodeQuotes.cs  
├── Models/DTOs/  
│ ├── EpisodeUpdateDto.cs  
│ ├── EpisodeWithQuotesDto.cs  
│ ├── QuoteCreateDto.cs  
│ ├── QuoteInlineDto.cs  
│ └── QuoteUpdateDto.cs  
├── Repo/  
│ └── Interfaces & Repositories  
├── Services/  
│ └── EpisodeService.cs  
│ └── EpisodeQuotesService.cs  

---

## 🔌 Endpoints

### Episodes

| Method | Route               | Description                      |
|--------|---------------------|----------------------------------|
| GET    | `/api/episodes`     | Get all episodes with quotes     |
| GET    | `/api/episodes/{id}`| Get specific episode by ID       |
| POST   | `/api/episodes`     | Add a new episode + quotes       |
| PUT    | `/api/episodes`     | Update episode info              |
| DELETE | `/api/episodes/{id}`| Delete episode and its quotes    |

### Episode Quotes

| Method | Route                     | Description                    |
|--------|---------------------------|--------------------------------|
| GET    | `/api/episodequotes`      | Get all quotes                 |
| GET    | `/api/episodequotes/{id}` | Get single quote by ID         |
| GET    | `/api/episodes/{id}/quotes`| Get all quotes for an episode |
| POST   | `/api/episodequotes`      | Add a new quote                |
| PUT    | `/api/episodequotes`      | Update a quote                 |
| DELETE | `/api/episodequotes/{id}` | Delete a quote                 |

---

## 📌 Key Features

- 🔄 **Flexible Updates** — Partial updates allowed on PUT
- 🔗 **Quote-to-Episode Linking** — By ID or by Title+Season
- 🧼 **DTO Separation** — Clean layers for GET, POST, PUT
- 🧪 **Fully Tested** — Manually verified endpoints during development
- 🌐 **Publicly Accessible** — Hosted via Cloudflare Tunnel

---

## 🧠 Inspiration
Built to flex ASP.NET Core architecture skills while paying homage to the greatest sitcom of all time. Yada yada yada... now it's real.

### ✍️ Author
Made with love by Matt Mortensen  
__“You can't be a little bit pregnant.”__ – Jerry Seinfeld

---

### 📄 License

This project is licensed under the MIT License.

You are free to use, modify, distribute, and even rebrand this API for your own use — just include the original license and credit when appropriate.