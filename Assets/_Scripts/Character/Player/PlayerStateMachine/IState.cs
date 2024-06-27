public interface IState
{
    /// <summary>
    /// 状態入る処理
    /// </summary>
    void Enter();
    
    /// <summary>
    /// 状態出る処理
    /// </summary>
    void Exit();

    /// <summary>
    /// ロジック更新処理
    /// </summary>
    void LogicUpdate();
    
    /// <summary>
    /// 固定間隔の物理演算処理
    /// </summary>
    void PhysicUpdate();
}
