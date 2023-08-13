using UnityEditor;

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
  public override void OnInspectorGUI()
  {
    Interactable interactable = (Interactable)target;
    if(target.GetType() == typeof(Interactable))
    {
      interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
      EditorGUILayout.HelpBox("EventOnlyInteract can only use UnityEvents", MessageType.Warning);
      if(interactable.GetComponent<Interactable>() == null)
      {
        interactable.useEvents = true;
        interactable.gameObject.AddComponent<InteractionEvent>();
      }
    }
    else
    {
      base.OnInspectorGUI();
      if (interactable.useEvents)
      {
        if (interactable.GetComponent<InteractionEvent>() == null)
          interactable.gameObject.AddComponent<InteractionEvent>();
      }
      else
      {
        if (interactable.GetComponent<InteractionEvent>() != null)
        {
          Destroy(interactable.GetComponent<InteractionEvent>());
        }
      }
    }
   
  }
}

