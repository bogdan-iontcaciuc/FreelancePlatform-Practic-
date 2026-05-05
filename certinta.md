# Cerință Proiect – Platformă Web de Anunțuri Freelancing

## Descriere Generală

Se cere realizarea unei aplicații web de tip platformă freelancing, unde utilizatorii pot publica și vizualiza anunțuri legate de servicii IT.

Platforma va permite două tipuri de anunțuri:

1. **Anunțuri de prestare servicii**  
   Publicate de programatori/freelanceri care oferă servicii.

2. **Anunțuri de cerere servicii**  
   Publicate de clienți care caută programatori pentru proiecte.

Aplicația trebuie realizată folosind două servicii separate:

- aplicație web frontend
- server backend REST API cu bază de date

---

## Arhitectura Sistemului

```text
Aplicație Web Blazor
        |
        | HTTP / REST API
        v
ASP.NET Core Web API
        |
        | Entity Framework Core
        v
Microsoft SQL Server
```

---

## Tehnologii Obligatorii

### Frontend

- C#
- Blazor
- HTTP Client pentru apelarea API-ului

### Backend

- C#
- ASP.NET Core Web API
- REST API
- Swagger / OpenAPI
- Entity Framework Core

### Bază de Date

- Microsoft SQL Server

---

## Structura Proiectului

```text
PlatformaFreelancing.sln
│
├── PlatformaFreelancing.Api
│
└── PlatformaFreelancing.Web
```

---

## Convenții de Denumire

Toate denumirile din proiect trebuie să fie în limba română, inclusiv:

- denumirile proiectelor
- denumirile folderelor
- denumirile fișierelor
- denumirile claselor
- denumirile metodelor
- denumirile proprietăților
- denumirile tabelelor din baza de date
- denumirile coloanelor din baza de date
- denumirile endpoint-urilor REST

Exemple:

```text
Utilizator.cs
Anunt.cs
AutentificareController.cs
AnunturiController.cs
ContextAplicatie.cs
```

Tabele:

```text
Utilizatori
Anunturi
```

---

## Funcționalități Minime

### Autentificare

Aplicația trebuie să permită:

- înregistrare utilizator
- autentificare utilizator
- logout

Endpoint-uri recomandate:

```http
POST /api/autentificare/inregistrare
POST /api/autentificare/login
GET  /api/autentificare/eu
```

---

## Utilizatori

Fiecare utilizator trebuie să aibă cel puțin:

- Id
- Nume
- Email
- ParolaHash
- DataInregistrarii

---

## Anunțuri

Fiecare utilizator autentificat poate crea anunțuri.

Fiecare anunț trebuie să conțină:

- Id
- Titlu
- Descriere
- TipAnunt
  - `OferServiciu`
  - `CautServiciu`
- Categorie
- Tehnologii
- PretSauBuget
- DataPublicarii
- UtilizatorId

---

## Funcționalități pentru Anunțuri

Backend-ul trebuie să expună endpoint-uri pentru:

```http
GET  /api/anunturi
GET  /api/anunturi/{id}
POST /api/anunturi
```

Frontend-ul trebuie să permită:

- afișarea listei de anunțuri
- vizualizarea detaliilor unui anunț
- crearea unui anunț nou de către un utilizator autentificat

Nu este necesară implementarea funcționalităților de editare sau ștergere a anunțurilor.

---

## Cerințe Backend

Backend-ul trebuie să:

- fie realizat în ASP.NET Core Web API
- expună endpoint-uri REST
- utilizeze Entity Framework Core
- se conecteze la Microsoft SQL Server
- returneze răspunsuri în format JSON
- includă validări simple pentru datele primite
- includă Swagger pentru testarea endpoint-urilor
- implementeze autentificare cu JWT

---

## Cerințe Frontend

Frontend-ul trebuie să:

- fie realizat în Blazor
- comunice cu backend-ul prin REST API
- permită înregistrare și autentificare
- afișeze lista de anunțuri
- afișeze detaliile unui anunț
- permită crearea unui anunț nou
- afișeze mesaje simple de eroare sau confirmare

---

## Bază de Date

Baza de date trebuie să conțină minim două tabele:

### Utilizatori

Pentru stocarea utilizatorilor aplicației.

### Anunturi

Pentru stocarea anunțurilor publicate.

Relație:

```text
Un utilizator poate avea mai multe anunțuri.
Un anunț aparține unui singur utilizator.
```

---

## Swagger

Backend-ul trebuie să includă Swagger/OpenAPI pentru documentarea și testarea endpoint-urilor.

Exemplu:

```text
https://localhost:5001/swagger
```

---

## Livrabile

Studentul trebuie să prezinte:

1. Codul sursă complet
2. Aplicația web funcțională
3. Serverul REST API funcțional
4. Baza de date Microsoft SQL Server
5. Documentația endpoint-urilor în Swagger
6. Demonstrarea funcționalităților principale

---

## Obiectivul Proiectului

Scopul proiectului este dezvoltarea unei aplicații full-stack simple în C#, folosind Blazor, ASP.NET Core Web API și Microsoft SQL Server.

Proiectul urmărește înțelegerea conceptelor de bază:

- arhitectură client-server
- REST API
- autentificare
- conectare la bază de date
- afișare și creare date într-o aplicație web