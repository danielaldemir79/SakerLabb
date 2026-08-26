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
| 4 |  |  |  |  |  |  |
| 5 |  |  |  |  |  |  |

Bevis (skärmbilder eller utdrag), numrerade efter fyndet ovan:
### Fynd 1, CodeQL statisk analys, före åtgärd

<img src="bevis/fynd-1-codeql-fore.png" alt="CodeQL-fynd 1 före åtgärd" width="700">

### Fynd 2, ZAP passiv analys, före åtgärd

<img src="bevis/fynd-2-zap-passiv-directory-browsing-fore.png" alt="ZAP-fynd Directory Browsing före åtgärd" width="700">

### Fynd 3, ZAP passiv analys, före åtgärd

<img src="bevis/fynd-3-zap-passiv-content-type-header-fore.png" alt="ZAP-fynd X-Content-Type-Options före åtgärd" width="700">

---

## 3. Prioritering

Rangordna fynden och motivera ordningen med allvarlighetsgrad, exponering och utnyttjbarhet. Vilket tar du först och varför?

*Skriv här.*

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
