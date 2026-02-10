using System;
using OnlineLibrary.Common;

public static class ExtensionClass
{
	//метод розширення
	public static void WhatAmIReading(this Item obj)
	{
		if(obj is Book)
		{
			Console.WriteLine($"\nYou read a book \"{obj.Title}\"");
		}
		else if (obj is Magazine)
		{
			Console.WriteLine($"\nYou read a magazine \"{obj.Title}\"");
		}
		else if ( obj is Article)
		{
            Console.WriteLine($"\nYou read a article \"{obj.Title}\"");
        }
		else 
		{
            Console.WriteLine("Invalid type");
        }
	}
}
