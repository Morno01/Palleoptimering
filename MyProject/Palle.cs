using System;


public class Palle
{
    var palle = new Palle
    {
        PalleBeskrivelse = "Test palle",
        Laengde = 1200,
        Bredde = 800,
        Hoejde = 150,
        Palletype = "Træ",
        MaksHoejde = 2800,
        MaksVaegt = 800m,
        Aktiv = true
    };

    // Validér
    var fejl = palle.Valider();
if (fejl.Any())
{
    Console.WriteLine("Fejl: " + string.Join(", ", fejl));
}

// ===== OPRET ELEMENT =====

var element = new Element
{
    ElementReferenceId = "DØR-001",
    Bredde = 800,
    Hoejde = 2100,
    Dybde = 50,
    Vaegt = 35m,
    MaaRoteres = "Ja"
};

// Tjek om element passer på palle
bool passer = palle.KanRummeElement(element);
Console.WriteLine($"Element passer på palle: {passer}");

// Tjek om element kan roteres
bool kanRoteres = element.KanRoteres();
Console.WriteLine($"Kan roteres: {kanRoteres}");

// ===== OPRET PLACERING =====

var placering = new Placering
{
    PalleId = palle.Id,
    ElementId = element.Id,
    Raekke = 1,
    Lag = 1,
    ErRoteret = false
};

Console.WriteLine(placering.BeregnPositionBeskrivelse());
// Output: "Række 1, Lag 1"

// ===== BRUG ROTATIONSREGEL =====

var rotationsRegel = new Rotations_Regel
{
    TilladVendeOpTilMaksVaegt = 70m,
    HoejdeBreddefaktor = 0.3m
};

bool maaVende = rotationsRegel.MaaVendeElement(element.Vaegt);
Console.WriteLine($"Må vende element (35kg): {maaVende}"); // True

bool skalRotere = rotationsRegel.SkalRoterePgaForhold(800, 2100, true);
Console.WriteLine($"Skal rotere pga. forhold: {skalRotere}"); // Sandsynligvis true

// ===== BRUG STABLINGSREGEL =====

var stablingsRegel = new Stablings_Regel
{
    MaksLag = 3,
    TilladStablingOpTilMaksHoejde = 1500,
    TilladStablingOpTilMaksVaegt = 70m
};

var (tilladt, stablFejl) = stablingsRegel.EvaluerStabling(
    aktuelLag: 2,
    totalHoejde: 2250,
    elementVaegt: 35m,
    erGeometriElementUnder: false
);

Console.WriteLine($"Stabling tilladt: {tilladt}");
if (!tilladt)
{
    Console.WriteLine("Fejl: " + string.Join(", ", stablFejl));
}
}

