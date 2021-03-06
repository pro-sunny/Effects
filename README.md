# Active Effects 

Задача: створити систему ефектів які можуть бути надані башням, монстрам або іншим спеціальним об'єктам. Якщо надані башням, ці ефекти можуть покращувати свої якості з новими рівнями башні. 

Наприклад: 
* [Башня] Атаки башні сповільнюють ціль на 10% на 3с. Кожна атака має 15% нанести додатково 50 дамага по сповільненим цілям. 
* [Башня] Ця башня і всі зв'язані башні отримують атаку отрутою. Отрута наносить 3 дамага кожну секунду протягом 5ти секунд. Ефект стакається до 3х разів. 
* [Башня] Кожен монстр який підійде на відстань 900 від цієї башні сповільнюється на 20% на 4 секунди. Коли сповільненя закінчує свою дію ціль отримає 20 шкоди і застуниться на 1с. 
* [Монстр] Коли цей монстр помирає всі башні в радіусі 300 сповільнюють свою атаку на 15% на 5секунд.

Це реалізовано наступним чином:
1. Створюється ActiveEffect (який є SerializedScriptableObject) в якому прописуються базові значення ефектів, а так само шанс і умови активації.
<img src="https://i.imgur.com/tb4qs3v.png" alt="drawing" width="350"/>
2. До створеного ActiveEffect приєднується EffectActivator (Prefab) який буде активуватись при виконань умов активації в ActiveEffect. Активуватись в данному випадку означає що цей префаб буде інстанційовано на цілі ефекту. 
<img src="https://i.imgur.com/hBD8apQ.png" alt="drawing" width="600"/>

3. EffectActivator в свою чергу може включати в себе інші EffectActivator для активації декількох еффектів одразу. До будь якого EffectActivator при необхідності додається компонент EffectVisualizer.

4. Після того як ефект було створено, він додається до виконався компонентом ActiveEffect, який має різіні реалізації, наприклад OnAttackEffect, OnCloseDeathEffect, OnDamageEffect.
<img src="https://i.imgur.com/cp3boZ5.png" alt="drawing" width="350"/>


Хоч ця система і виконує свою задачу, я розумію що вона далека від ідеалу. Одна з проблем яку я бачу, це немало залежностей в редакторі, для цього я зробив би кастомний інспектор який знає все про ефекти, може їх редагувати, створювати і видаляти. В ідеалі ми просто додаємо потрібний компонент до виконавця і додаємо ScriptableObject ефекта. Приклад інспектора можна взяти з дефолтного одіновського інспектора для речей в РПГ. 
