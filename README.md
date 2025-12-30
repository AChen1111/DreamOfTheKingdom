# DreamOfTheKingdom 项目实现说明

## 项目概览
这是一个基于 Unity 的回合制卡牌战斗 + 地图房间推进的项目。核心循环是：菜单进入地图 → 选择房间加载战斗场景 → 回合制战斗（卡牌/敌人行动）→ 胜负结算与卡牌奖励 → 返回地图继续推进。

## 使用的技术与插件
- Unity (C#)
- Addressables：异步加载场景与卡牌数据（如 `SceneLoadManager`、`CardManager`）
- DOTween / DOTween Pro：卡牌抽取动画、按钮缩放、特效颜色等
- Spine Runtime：角色/动画支持（`Assets/Spine`）
- TextMesh Pro：UI 文本显示
- UI Toolkit：血条与状态 UI（`HealthBarControler`）
- UnityEngine.Pool：对象池管理卡牌实例（`PoolTool`）

## 核心系统与实现细节

### 1) 场景与流程管理
- `SceneLoadManager` 使用 Addressables 进行加/卸载场景（加法加载），并在房间切换后广播事件。
- `GameManager` 负责更新地图房间状态（当前房间已访问、同列锁定、可达房间解锁）。
- `UIManager` 统一管理主菜单、战斗、胜利/失败等面板显示。

### 2) 地图生成与持久化
- `MapConfigSO` 用 `RoomBlueprint` 定义每列房间的数量区间与房间类型。
- `MapGenerator` 根据配置随机生成房间位置与连线，并将布局序列化到 `MapLayoutSO`。
- `MapLayoutSO` 保存房间坐标、状态、连接关系以及连线位置，支持场景重进时还原地图。
- `Room` 作为地图节点，点击可触发加载房间事件。

### 3) 回合制战斗
- `TurnBaseManager` 是回合状态机（玩家回合/敌人回合/战斗结束），通过事件驱动卡牌抽取和敌人行动。
- `EnemyManager` 维护敌人列表，并在敌人回合按顺序执行行动；当敌人全灭时广播胜利。
- `CharacterBase` 处理生命、护盾、Buff/DeBuff 逻辑；`Player` 在回合开始重置能量与护盾。

### 4) 卡牌与效果系统
- `CardDataSO` 记录卡牌基础数据与效果列表，`CardLibrarySO` 维护卡库与数量。
- `CardManager` 通过 Addressables 加载 `CardDataSO`，并用 `PoolTool` 复用卡牌对象。
- `CardDeck` 维护抽牌堆/弃牌堆/手牌，抽牌时触发布局动画，并广播牌堆数量变化。
- `Card` 在拖拽/释放时判定目标与费用，执行 `Effect` 列表中的效果。
- `Effect` 为抽象 ScriptableObject，派生实现（如 `DamageEffect`、`DefenseEffect`、`HealEffect`、`DrawCardEffect`、`AddManaEffect`），通过配置形成不同卡牌行为。

### 5) 事件与数据驱动
- `BaseEventSO<T>` + `BaseEventListener<T>` 形成 ScriptableObject 事件通道，实现模块解耦。
- `IntVariable` 将运行时数值（如 HP、Mana、Defense）封装成 ScriptableObject，并在变化时触发事件更新 UI。

### 6) UI 与表现
- `GamePlayPanel` 监听抽牌/弃牌/能量/回合事件，更新数值与回合提示。
- `HealthBarControler` 使用 UI Toolkit 动态展示血量、护盾、Buff 图标。
- `UIButtonDoTweenScaler` 使用 DOTween 实现按钮交互缩放反馈。

## 设计模式与架构特征
- 单例模式：`UIManager`、`EnemyManager`、`CardGetManager` 等用于全局访问。
- 观察者/事件通道：ScriptableObject 事件（`BaseEventSO`）与 `UnityEvent` 监听解耦系统。
- 策略模式：`Effect` 作为可配置行为策略，卡牌与敌人动作通过数据驱动切换。
- 对象池：`PoolTool` 管理卡牌实例，降低运行期开销。
- 数据驱动：卡牌、房间、地图、敌人动作均以 ScriptableObject 配置为核心。

## 主要代码入口参考
- 场景与流程：`Assets/Scripts/Manager/SceneLoadManager.cs`
- 地图系统：`Assets/Scripts/Rooms/Mono/MapGenerator.cs`
- 回合系统：`Assets/Scripts/Manager/TurnBaseManager.cs`
- 卡牌系统：`Assets/Scripts/Card/Mono/CardDeck.cs`, `Assets/Scripts/Card/Mono/Card.cs`
- 事件系统：`Assets/Scripts/Events/ScripctsObject/BaseEventSO.cs`
- 角色与敌人：`Assets/Scripts/Character/CharacterBase.cs`, `Assets/Scripts/Character/Enemy/Enemy.cs`

## 扩展建议
- 新增卡牌：创建新的 `Effect` ScriptableObject，再在 `CardDataSO` 中组合配置。
- 新增房间类型：在 `RoomType` 中扩展枚举，并配置对应 `RoomDataSO`。
- 新增敌人行为：在 `EnemyActionSO` 增加权重配置，或派生新的 `Effect`。
