using Newtonsoft.Json;

namespace RoastHiveMvc.Controllers;

public static class Session{

    // Serialize a object to a session with a key
    public static void SetAsJson(this ISession session, string key, object value){
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    // De-serialize a object from a session for a key
    public static T? GetFromJson<T>(this ISession session, string key){
        var value = session.GetString(key);
        return value == null ? default(T): JsonConvert.DeserializeObject<T>(value);
    }
}