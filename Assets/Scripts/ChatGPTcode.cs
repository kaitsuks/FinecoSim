using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public Person CreateRandomPerson()
{
    Person newPerson = new Person();
    // esim. satunnaiset arvot
    newPerson.Occupation = UnityEngine.Random.value > 0.5f ? "hairdresser" : "restaurantWorker";
    newPerson.Wage = UnityEngine.Random.Range(1500f, 3000f);
    return newPerson;
}

public class Company : MonoBehaviour
{
    public string Type { get; private set; } = "Hairdresser";
    public float BasePrice { get; set; } = 20f;
    public float Reputation { get; set; } = 0.5f; // 0–1

    public float GetPriceWithTax(float vatRate)
    {
        return BasePrice * (1 + vatRate);
    }
}

2.State.cs

Tarkoitus: hallitus / valtio, joka voi säätää verot ja tulonsiirrot.

Kentät: alv eri sektoreille, palkat, peruspäiväraha, tuet, menot, julkisen velan koko, hallituksen kannatusprosentti.

Virheitä/puutteita:

float "marginaalivero" = 1f; → tämä ei käänny (lainausmerkit kentän nimen ympärillä). Pitää olla esim. float marginalTax = 1f;.

Kentät ovat private, niitä pitäisi tarjota propertyjen kautta.

Tarvittaisiin metodeja: SetTaxRate(), CollectTaxes(), PayBenefits() jne.