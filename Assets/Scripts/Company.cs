using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class Company : MonoBehaviour
{
    //hairdresser
    //method: CutHair
    //reasons for visiting barber
    //  1. too much/long hair
    //  2. re-styling of hair
    //  3. special ocassion
    //  4. habit
    //  reasons for not visiting barber
    //  1. Has no hair
    //  2. Hair has not grown enough to be cut
    //  3. Is old and needs no hair cut
    //  4. The hair is long and growth does not matter
    //  5. Work does not require "good" hair so needs no visit to barber
    //  6. The price is too expensive

    //  notice: hair growth is individual

    //float hairGrowth = 2f;
    //men_price_after_tax = addedValueTaxHairdresser * men_price
    //women_price_after_tax = addedValueTaxHairdresser * women_price
    //string reputationHairddresser = "good";
    //if hairShort -> not cut hair
    //price = addedValueTaxHairdresser * originalPriceHairdresser
    // 

    //hotel
    //method: CustomerHappy
    //method: CustomerUnhappy
    //reason for not using hotel:
    //  1. too expensive
    //  2. has no need for hotel
    //  3. hotel is not good enough
    //  4. no hotel exists
    //reason for using hotel:
    //  1. tourism (foreign and native)
    //  2. due to work/education not having own housing

    //food
    //eating lunch/dinner/food at a restaurant
    //drinking coffe/coke etc.

    //alcohol
    //alcohol consumed in restaurant
    //alcohol consumed at home
    //alcohol bought from store
    //alcohol brought from Estonia
    //alcohol prepared at home 
    //reduction of alcohol consumption

    //parameter
    //string reputationHotel = "5 star"
    //float hotelPrice = 100f;
    //float wagesEmployeesHotel = 100f;
    //float taxHotel = 100f;
    //string wagesEmploymentHotelDate = "2nd day"
    //price = addedValueTaxHotel* originalPriceHotel

    //businessmanagement
    //if the business has good economy
    // 1. employing more staff
    // 2. founding a branch hotel/barbershop
    // 3. probably paying more taxes
    //if the business has bad economy
    // 1. reducing staff
    // 2. shutting down main hotel/barber
    // 3. shutting down brach hotel/barber
    // 4. probably paying less taxes
}
