using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Method)]
public class AddToDataBase : Attribute
{
    
}

[AttributeUsage(AttributeTargets.Method)]
public class RemoveFromDataBase : Attribute
{

}