public static class NPCData
{
    public static bool isComfortValPositive;
    private static int npcComfort = 1;
    private static int maxNpcComfort = 4;
    private static int minNpcComfort = -4;

    public static void ResetData()
    {
        npcComfort = 1;
        maxNpcComfort = 4;
        minNpcComfort = -4;
    }
    public static void AddToComfortValue(int additionalVal)
    {
        if(additionalVal > 0)
        {
            isComfortValPositive = true;
        }
        else
        {
            isComfortValPositive = false;
        }

        if (npcComfort > minNpcComfort && npcComfort < maxNpcComfort)
        {
            npcComfort += additionalVal;
        }
        else if (npcComfort == minNpcComfort && additionalVal > 0)
        {
            npcComfort += additionalVal;
        }
        else if (npcComfort == maxNpcComfort && additionalVal < 0)
        {
            npcComfort += additionalVal;
        }
    }

    public static void SetMaxComfortValue(int distractingValue)
    {
        npcComfort -= distractingValue;
        maxNpcComfort -= distractingValue;
    }

    public static int GetComfortValue()
    {
        return npcComfort;
    }

    public static string ReturnComfortValueAsString()
    {
        if (npcComfort < 0)
        {
            return "" + npcComfort;
        }
        else if (npcComfort == 0) 
        {
            return "" + npcComfort;
        }
        else
        {
            return "+" + npcComfort;
        }
    }
}