# Telecom Campaign Management System

Ova aplikacija predstavlja  rešenje za upravljanje i analizu telekomunikacionih marketinških kampanja. Izgrađena je primenom **Clean Architecture** (Čiste arhitekture) i **Domain-Driven Design (DDD)** principa u .NET okruženju, sa fokusom na potpunu nezavisnost slojeva, JWT autentifikaciju i lakoću integracije sa eksternim sistemima.

---

 Arhitektura Sistema

Projekat je podeljen na četiri jasno razdvojena slojeva:
* **Domain:** Sadrži osnovne biznis entitete (`Agent`, `Nomination`, `CampaignPurchase`) izolovanih od eksternih biblioteka.
* **Application:** Sadrži kompletnu biznis logiku, DTO-ove i interfejse (Ugovore) koji diktiraju pravila sistema.
* **Infrastructure:** Implementira repozitorijume, povezivanje sa SQL Server bazom podataka (EF Core) i eksterne integracije (**CountryInfoService WCF/SOAP klijent** za validaciju meta kampanje).
* **Api (Presentation):** REST API sa izloženim sigurnim endpointima za klijente i centralizovanim Middleware-om za upravljanje greškama.

---

##  Uputstvo za Pokretanje i Kreiranje Baze
1. Otvorite `appsettings.json` unutar **Api** projekta i prilagodite Connection String vašem lokalnom SQL Serveru:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=VAŠ_SERVER;Database=TelecomCampaign;Trusted_Connection=True;TrustServerCertificate=True;"
   }
2. U package manager console ukucajte Update-Database

## Napomena
 U samom tekstu zadatka je navedeno ogranicenje od 5 unosa po agentu u toku dana, medju poslednjim commit-ovima dodao sam i ogranicenje
 koje se odnosi na to da to moraju biti razliciti klijenti (u toku jednog dana)
