# Wheelchair simulator

Een VR rolstoel simulator. Het doel van het project is om mensen die nog nooit in een rolstoel geleeft hebben en zo realisch mogelijke ervaring aan te bieden om zelf te ervaren wat het is om in een rolstoel te leven.

## Branches

### Master

Hierin bevind zich de basis van ons project, maar we hebben hier niet op verder gewerkt. 

### HakanTest

Dit is de branch waar de uitgewerkte versie van ons project zich bevindt. De reden waarom dat dit een aparte branch is, is omdat we een aantal scripts wouden testen zonder in merge conflict te komen. Dit is ook de default branch van onze github repository. 

## Scène's 

In ons project zijn er drie belangrijke scène's 

### Main

Dit is de belangrijkste Scène in ons project hier komt alles samen. Dit is de scène waarin dat de gebruiker mag rond rijden in een rolstoel. 

### Houses

Dit is een test Scène gebouwd om de huizen te ontwerpen, hier in zitten verschillende models van huizen maar de meeste waren slecht, we hebben ze niet verwijderd zodat we kunnen leren van onze design fouten in het verleden. Een huis werd hierin eerst ontworpen en dan pas gexporteerd als prefab om te worden gebruikt in de Main scène. 

### WheelchairScriptTestScene

Dit is een Scène dat door ons werd gebruikt om rolstoel scripts uit te testen.

## Scripts
### WheelchairController
Script voor de controle van de rolstoel. Het grootste deel van de klasse wordt niet meer gebruikt, er zijn nog veel restanten van eerdere expermenten met de controle.

### GripController
Zorgt dat je met de handen iets kunt vastnemen en kasten open/dicht doen.

### Player
Alternatief controller script, liet toe om met de controller naar een locatie te wijzen en daar dan heen te teleporteren om makkelijk rond te geraken tijdens testen.
