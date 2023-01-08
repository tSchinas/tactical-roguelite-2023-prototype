using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneState : BattleState
{
    ConversationController conversationController;
    ConversationData data;

    protected override void Awake()
    {
        base.Awake();
        conversationController = owner.GetComponentInChildren<ConversationController>();
        data = Resources.Load<ConversationData>("Conversations/TestConversation");//currently only loads test conversation
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (data)
            Resources.UnloadAsset(data);
    }

    public override void Enter()
    {
        base.Enter();
        conversationController.Show(data);
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        ConversationController.completeEvent += OnCompleteConversation;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ConversationController.completeEvent -= OnCompleteConversation;
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        base.OnFire(sender, e);
        conversationController.Next();
    }

    void OnCompleteConversation(object sender, System.EventArgs e)
    {
        owner.ChangeState<SelectUnitState>();
        Debug.Log("Changed to Select Unit State.");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
