public static class ActionHandlers {
    public static Dictionary<string,Delegate> actionDictionary = new Dictionary<string, Delegate>();

    public static void addActionToDic(string actionID, Delegate s){
        actionDictionary.Add(actionID,s);
    }

    public static Delegate getAction(string actionID){
       return actionDictionary[actionID];
    }
    public static bool hasAction(string actionID){
        return actionDictionary.ContainsKey(actionID);
    }

    public static void RegisterActions(){
        
     

    }
}