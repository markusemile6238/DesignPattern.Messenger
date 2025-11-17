
using DP.Activator;
using DP.Activator.model;


Person magalie = new Person("Magalie");
Person max = new Person("Max");
Person albert = new Person("Albert");



max.SendMessage(message:"Hello every Body");
string idMagalie = magalie.Id;
Console.WriteLine(idMagalie);
max.SendMessage(message:"specialement pour toi magalie",idFriend:idMagalie);
albert.SendMessage(message: "Ah oui tous pour Magalie", idFriend: idMagalie);







