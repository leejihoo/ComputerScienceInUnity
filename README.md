# ComputerScienceInUnity
## 1. 자료구조
### 1.1 List
|반복횟수|함수명|ArrayList|List\<object\>|List\<int\>|LinkedList\<int\>|
|-------|------|----------|-------------|------------|-----------------|
|400만|Add|2620ms|2673ms|30ms|2854ms|
|50|Remove|2629ms|2616ms|1343ms|2966ms|
|50|Insert|1628ms|1572ms|795ms|1752ms|
|1|Contains|498ms|491ms|250ms|571ms|
* 실험조건1: Remove와 Insert는 List.Count가 400만이고 List의 중간 위치에서 실행
* 실험조건2: Remove와 Insert를 할 때 특정 인덱스나 노드를 사용하지 않고, IndexOf & Find로 탐색한 뒤 실행
* 실험조건3: LinkedList는 Add 대신 AddLast, Insert 대신 AddAfter을 사용
* 실험조건4: Contains는 LIst.Count가 4000만이고, List의 마지막 수를 탐색
* 실험조건5: List에 넣는 값의 자료형은 int type이다.

> [!note]
> 대체로 성능은 아래와 같다.
> 1. List\<int\>
> 2. List\<object\>, ArrayList
> 3. LinkedList\<int\>

#### 1.1.1 ArrayList와 List\<object\>가 List\<int\>보다 성능이 안좋은 이유
ArrayList와 List\<object\>의 함수들이 작동할 때 int type이 object type으로 바뀌는 박싱, object type을 int type으로 바뀌는 언박싱이 일어나기 때문이다. <br>
박싱이 일어날 때 박싱된 값은 힙에 저장하고, 박싱된 값을 가르키는 주소 값을 스택에 저장하여 성능이 저하된다.  <br>
언박싱이 일어날 때 박싱된 값이 언박싱할 타입과 일치하는지 확인하는 과정과 stack에 새로운 값을 복사하는 과정으로 인해 성능이 저하된다.  <br>
[마이크로소프트에서 알려주는 박싱과 언박싱](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/types/boxing-and-unboxing)  <br>

#### 1.1.2 LinkedList<int>의 성능이 제일 안좋은 이유
추가에 대한 시간 복잡도는 모두 O\(1\)인데 박싱&언박싱도 안 발생하는 LinkedList<int>가 제일 느린 이유는 무엇일까? <br>
그것은 추가가 일어날 때마다 새로운 노드 객체를 힙에 생성하고 스택에 주소값을 저장한 뒤 다른 노드들과 연결하는 과정(포인터 참조)에서 비용이 발생하기 때문이다. <br>
삽입과 삭제에 대한 시간 복잡도는 연결리스트는 O\(1\), 배열은 O\(n\)이다. <br>
그렇다면 remove와 insert의 성능이 LinkedList<int>가 가장 좋아야된다고 예상할 수 있다. <br>
하지만 위의 실험에서는 연결리스트는 모두 성능이 좋지 않았다. 그 이유는 배열과 연결리스트의 순회 비용 차이에 있다. <br>
실험 조건2에서 알 수 있듯이 Insert와 Remove를 할 때 특정 인덱스나 노드 정보를 안 상태에서 작동하는게 아니라 IndexOf & Find로 탐색한 뒤 실행했다. <br>
연결리스트와 배열 모두 탐색에 대한 시간 복잡도는 O\(n\)이지만 배열은 연속된 메모리 주소를 가지고 있어서 탐색과정에서 cache hit가 일어나지만
연결리스트는 비연속적인 메모리 주소를 가지고 있어서 탐색과정에서 cache miss가 일어나기 때문에 같은 시간 복잡도라도 실상은 연결리스트가 더 성능이 좋지않다. <br>
이 실험에서 Insert와 remove의 총 시간복잡도는 배열은 O\(n^2\), 연결리스트는 O\(n\)이지만 위에 말한 순회 비용에 대한 차이가 성능 차이를 낸다. <br>
그렇다면 연결리스트는 언제사용하면 좋을까? 특정 조건을 만족하면 배열보다는 연결리스트의 성능이 좋은 경우가 있다. <br> 
특정 조건이란 맨 앞이나 맨 뒤 또는 이미 위치를 알고 있어서 순회가 필요가 없는 경우에는 연결리스트가 배열보다 성능이 좋다. <br>
근데 C#의 stack은 배열, Queue는 순환 배열로 구현 되어 있다. 앞서 말한대로 LinkedList는 메모리 친화적이지 않기 때문에 C#에서는 stack과 queue를 배열로 구현하는 듯하다. <br>
stack은 선입후출이기 때문에 맨뒤 값만 없애기 때문에 배열의 resizing이 필요없어 O\(1\)만큼 걸리지만 queue는 맨 앞에서 제거되면 배열의 resizing이 필요해 O\(n\)만큼 걸리는데
이를 순환배열 구조를 사용해서 성능 문제를 해결한 것 같다. <br>
[C# Stack](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.stack-1?view=net-8.0)  <br>
[C# Queue](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.queue-1?view=net-8.0)  <br>
