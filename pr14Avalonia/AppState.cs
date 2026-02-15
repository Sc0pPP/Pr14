using System;
using System.Collections.Generic;
using pr14Avalonia.Models;

namespace pr14Avalonia;

public static class AppState
{
    public static User CurentUser { get; set; }
    public static Movie CurrentMovie { get; set; }
    public static List<Session> CurentSession=new List<Session>();
    public static   Session ChoiceSession { get; set; }
    public static List<Ticket> Tickets { get; set; }

    static void  SpecialForMasha()
    {
        if (CurentUser.Login == "даня"){
            if (CurentUser.Login == "дурак")
            {
                if (CurentUser.Login == "козел")
                {
                    Console.WriteLine("МашаПрава(while(true))");
                }
            
            }
            
        }
    }
     
}