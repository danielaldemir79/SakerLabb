# Labbrapport: praktisk laboration

*Kunskapskontroll 2, IT-säkerhet för utvecklare. Fyll i mallen och lämna in som PDF tillsammans med länken till ditt repo. Riktlängd två till tre sidor.*

**Namn:**
**Datum:**
**Repo (länk till din fork):**
**Applikation som analyserades:**

---

## 1. Kort om applikationen och analysen

Beskriv i några meningar vilken app du analyserade, vad den gör och hur du genomförde analysen. Ange vilka verktyg du använde och hur du körde dem (CodeQL default setup med språk C#, ZAP passiv och aktiv skanning mot vilken adress).

*Skriv här.*

---

## 2. Fem fynd

Fyll i tabellen. Minst ett fynd ska komma från statisk analys (CodeQL) och minst ett från dynamisk analys (ZAP). Spara bevis i form av skärmbild eller rapportutdrag och hänvisa till det per fynd.

| Nr | Källa (CodeQL/ZAP) | Regel-id eller alert | Allvarlighet (+ confidence för ZAP) | Fil och rad eller URL | Verkligt eller falskt positivt | Motivering (2–4 meningar) |
|----|--------------------|----------------------|-------------------------------------|-----------------------|--------------------------------|---------------------------|
| 1 | CodeQL | `cs/xml/insecure-dtd-handling` | Critical | `SakerLabb.Web/Services/ImportService.cs`, rad 27 | Verkligt | Appen tar emot XML-text från användaren. Inställningarna gör att XML-texten kan be appen läsa information från andra platser. En angripare kan därför försöka få appen att läsa något den inte borde läsa. |
| 2 | ZAP | Directory Browsing | Medium, confidence: Medium | `http://localhost:5080/files/` | Verkligt | Appen visar en lista över filer utan att användaren behöver logga in. Filerna innehåller information som inte borde vara öppen för alla. |
| 3 | ZAP | X-Content-Type-Options Header Missing | Low, confidence: Medium | `http://localhost:5080/app.css` | Verkligt | Appen saknar en säkerhetsinställning som säger åt webbläsaren att använda filens riktiga typ. Utan den kan webbläsaren i vissa fall tolka innehållet på fel sätt. |
| 4 | ZAP | Remote OS Command Injection | High, confidence: Medium | `http://localhost:5080/diagnostik/ping`, parameter `host` | Verkligt | ZAP lyckades få appen att köra ett extra Windows-kommando och läsa innehåll från en fil på datorn. En angripare kan därför försöka läsa filer eller köra andra kommandon på servern. |
| 5 | ZAP | Cross Site Scripting (Reflected) | High, confidence: Medium | `http://localhost:5080/account/login`, parameter `username` | Verkligt | ZAP:s testkod kom tillbaka från inloggningssidan utan att göras ofarlig. En angripare kan därför försöka få skadlig kod att köras i en användares webbläsare. |

Bevis (skärmbilder eller utdrag), numrerade efter fyndet ovan:
### Fynd 1, CodeQL statisk analys, före åtgärd

<img src="bevis/fynd-1-codeql-fore.png" alt="CodeQL-fynd 1 före åtgärd" width="700">

### Fynd 2, ZAP passiv analys, före åtgärd

<img src="bevis/fynd-2-zap-passiv-directory-browsing-fore.png" alt="ZAP-fynd Directory Browsing före åtgärd" width="700">

### Fynd 3, ZAP passiv analys, före åtgärd

<img src="bevis/fynd-3-zap-passiv-content-type-header-fore.png" alt="ZAP-fynd X-Content-Type-Options före åtgärd" width="700">

### Fynd 4, ZAP aktiv analys, före åtgärd

<img src="bevis/fynd-4-zap-aktiv-command-injection-fore.png" alt="ZAP-fynd Command Injection före åtgärd" width="700">

### Fynd 5, ZAP aktiv analys, före åtgärd

<img src="bevis/fynd-5-zap-aktiv-xss-fore.png" alt="ZAP-fynd XSS före åtgärd" width="700">

---

## 3. Prioritering

**1. Remote OS Command Injection**

Jag tar detta fynd först eftersom ZAP lyckades köra ett extra Windows kommando utan inloggning. Det visar att felet går att använda och att skadan kan bli stor. En angripare kan försöka läsa filer eller köra andra kommandon på servern.

**2. Directory Browsing**

Jag tar detta som nummer två eftersom vem som helst kan öppna fillistan utan att logga in. Filerna innehåller lösenord och personuppgifter. Felet är mycket enkelt att utnyttja eftersom det räcker att öppna en adress i webbläsaren.

**3. Osäker XML-hantering**

CodeQL bedömer fyndet som Critical. Appen tar emot XML från användaren och XML läsaren tillåter funktioner som kan läsa information från andra platser. Felet kan orsaka stor skada, men kräver att angriparen skickar särskilt skapad XML.

**4. Cross Site Scripting**

ZAP visade att testkod kom tillbaka på inloggningssidan. En angripare kan försöka få kod att köras i en annan användares webbläsare. Jag placerar fyndet efter de tre första eftersom angreppet normalt kräver att en användare öppnar skadligt innehåll.

**5. Saknad X-Content-Type-Options-header**

Detta placeras sist eftersom ZAP bedömer risken som Low. Säkerhetsinställningen bör finnas, men den saknade inställningen ger inte ensam samma direkta åtkomst till servern eller känsliga uppgifter som de andra fynden.

---

## 4. Åtgärder (minst tre)

Använd mönstret nedan per åtgärdat fynd. Varje åtgärd ska gå att spåra tillbaka till ett fynd i tabellen ovan, och beviset efter ska vara en **ny körning av verktyget**, inte din egen kod.

### Åtgärd 1

```
Fynd:        (nr och regel-id/alert från tabellen ovan)
Plats:       (fil och rad, eller URL)
Bevis före:  (skärmbild eller rapportutdrag som visar fyndet)
Bedömning:   (verkligt eller falskt positivt, kort motiverat)
Åtgärd:      (vad du ändrade, med commit-hash)
Bevis efter: (ny körning: CodeQL-alerten står som Fixed, eller ZAP-larmet är borta ur den nya rapporten)
```

### Åtgärd 2

```
Fynd:
Plats:
Bevis före:
Bedömning:
Åtgärd:
Bevis efter:
```

### Åtgärd 3

```
Fynd:
Plats:
Bevis före:
Bedömning:
Åtgärd:
Bevis efter:
```

---

## 5. Eventuella bortval

Om du valt att inte åtgärda ett fynd, skriv ned tre saker per bortval: risken, motivet och den kompenserande kontrollen. Sätt gärna ett datum för omprövning.

*Skriv här, eller skriv "inga bortval".*
