# Snake game in Godot engine

### Code review kódu
Git repozitář s kódem - https://github.com/Seeyappaja/BrokenSnake/blob/main/main.cs

Kód rozhodně splnil účel a původní rozbitý kód hada značně zjednodušil, nicméně máme k němu pár poznámek.
Jedna z do očí bijících věcí, která se mohla zlepšit je že se směrové instrukce nepřevedly na enum. 
To by přispělo k bezpečnosti kódu a nehrozilo by, že by se někdo přepsal ve směrové instrukci.
Další věc je nadměrné používání statických metod v objektově orientovaném programovacím jazyku.
Velká část těchto metod by mohla být bez problému převedena na metody jednotlivých tříd.
Jde vidět, že tento kód refraktoroval nějaký Python fanatik.
Poslední menší výtka, kterou ke kódu máme, je menší stylistický punk. velké písmenka na začátku názvů metod,
constanty značené jako proměnné, etc. Se vším všudy je ale refraktorizace kódu rozhodně zlepšení oproti původnímu.

Při použití kódu v Godotu se muselo hodně metod upravit a ohnout pro potřeby Godotu.
Kód tedy spíš vychází z tohoto refraktorovaného, zanechává nějaké metody z původního.
