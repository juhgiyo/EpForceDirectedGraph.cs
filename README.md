EpForceDirectedGraph.cs
=======================
#### A 2D/3D force directed graph algorithm in C# ####


Introduction
------------
This project was started after I was inspired by [Springy by Dennis Hotson](https://github.com/dhotson/springy) and [the Wikipedia article](http://en.wikipedia.org/wiki/Force-directed_graph_drawing).
It comes along with a demo to show how the agorithm execute as similar to Dennis Hotson's [Online Demo](http://dhotson.github.io/springy/demo-simple.html).


Nuget Package
------------
[Nuget Package](https://www.nuget.org/packages/EpForceDirectedGraph.cs/)


Basic Usage
------------
### Graph ###
  
You first need a `Graph` instance. :  

```c#
Graph m_fdgGraph = new Graph();   
```
This `Graph` instance will be used to operate your force-directed graph structure such as adding/removing new node, adding/removing edges, etc.  

### Node Operation ###

#### How do I add a node/nodes? ####
Simply to add new node with a name in the graph:  

```c#
Node newNode = m_fdgGraph.CreateNode("some node label");
```

You can also add new node with `NodeData` by modifying node's label and/or mass:  

```c#
NodeData data = new NodeData();
data.label = "some node label";
data.mass = 3.0f; // Optional
Node newNode = m_fdgGraph.CreateNode(data);
```

To add a node to the graph by manually creating node and node data  

```c#
NodeData data=new NodeData();
data.mass = 3.0f;
data.label = "some node label" 
Node newNode = new Node("Unique ID", data);
Node createdNode = m_fdgGraph.AddNode(newNode);
```

You may also bulk-add new nodes with list of `string` or `NodeData`:  

```c#
List<string> nodeNames= new List<string>();
...
m_fdgGraph.CreateNodes(nodeNames);
 
// OR

List<NodeData> nodeDatas =new List<NodeData>();
...
m_fdgGraph.CreateNodes(nodeDatas); 
```

After adding the nodes to the graph, you can easily get the node instance, you added, by its `label`:  

```c#
Node node = m_fdgGraph.GetNode("some node label");
```

#### How do I remove/detach a node? ####
To remove a node from the graph:  

```c#
Node node = m_fdgGraph.GetNode("some node label");
if(node != null)
   m_fdgGraph.RemoveNode(node);
```

To detach all the edges from a node (_**Note**: The node will still exist in the graph_):  

```c#
Node node = m_fdgGraph.GetNode("some node label");
if(node != null)
   m_fdgGraph.DetachNode(node);
```

### Edge Operation ###

#### How do I add an edge/egdes? ####
After adding the nodes first, you can connect the two nodes by creating an edge:  

```c#
Node node1 = m_fdgGraph.GetNode("node1");
Node node2 = m_fdgGraph.GetNode("node2");

EdgeData data= new EdgeData();
data.label = "node1"+"-"+"node2";
data.length = 60.0f;
Edge newEdge = m_fdgGraph.CreateEdge(node1, node2, data);
```

You can also create new `Edge` by giving node's `ID` (which is unique id given on node creation) directly instead of node instances:  

```c#
Node node1 = m_fdgGraph.GetNode("node1");
Node node2 = m_fdgGraph.GetNode("node2");

EdgeData data= new EdgeData();
data.label = "node1"+"-"+"node2";
data.length = 60.0f;

Edge newEdge = m_fdgGraph.CreateEdge(node1.ID, node2.ID, data);
```

To add an edge to the graph by manually creating edge and edge data  

```c#
Node node1 = m_fdgGraph.GetNode("node1");
Node node2 = m_fdgGraph.GetNode("node2");

EdgeData data=new EdgeData();
data.label = "node1"+"-"+"node2";
data.length = 60.0f; 
Edge newEdge = new Edge("Unique ID", node1, node2, data);
Edge createdEdge = m_fdgGraph.AddEdge(newEdge);
```

You may also bulk-add new edges with the list of the pair of first node's Unique ID string, second node's Unique Id string, or the list of the first node's Unique ID string, second node's Unique Id string and `EdgeData` for the edge:  

```c#
// First string is first node's Unique ID and second string is second node's Unique ID
List<Pair<string,string>> edges= new List<Pair<string,string>>();
...
m_fdgGraph.CreateEdges(edges);
 
// OR

List<Triple<string,string,EdgeData>> edges =new List<Triple<string,string,EdgeData>>();
...
m_fdgGraph.CreateEdges(edges); 
```

After adding the edges to the graph, you can easily get the edge instance, you added, by its `label`:  

```c#
Edge edge = m_fdgGraph.GetEdge("node1-node2");
```

You can also get the list of edges connected to a node as below:  

```c#
Node node1 = m_fdgGraph.GetNode("node1");
List<Edge> edgesConnected = m_fdgGraph.GetEdges(node1);
```

Finally, you can get the list of edges connected between two nodes by:  

```c#
Node node1 = m_fdgGraph.GetNode("node1");
Node node2 = m_fdgGraph.GetNode("node2");
List<Edge> edgesConnected = m_fdgGraph.GetEdges(node1, node2);
```

#### How do I remove an edge? ####
To remove an edge from the graph:  

```c#
Edge edge = m_fdgGraph.GetEdge("node1-node2");
if(edge != null)
   m_fdgGraph.RemoveEdge(edge);
```

### ForceDirected2D/ForceDirected3D ###

`ForceDirected2D` or `ForceDirected3D` is the calculation class of physics for force-directed graph. The instance of `ForceDirected2D/3D` will take in `Graph` (which is logical structure of force-directed graph), and will be inserted to the instance of the `Renderer`.  

To create `ForceDirected2D/3D` to calculate the physics for your force-directed graph:  

```c#
float stiffness = 81.76f;
float repulsion = 40000.0f;
float damping   = 0.5f;

// 2D Force Directed
ForceDirected2D m_fdgPhysics = new ForceDirected2D(m_fdgGraph // instance of Graph
                                                   stiffness, // stiffness of the spring
                                                   repulsion, // node repulsion rate 
                                                   damping    // damping rate  
                                                   );
// OR

// 3D Force Directed
ForceDirected3D m_fdgPhysics = new ForceDirected3D(m_fdgGraph // instance of Graph
                                                   stiffness, // stiffness of the spring
                                                   repulsion, // node repulsion rate
                                                   damping    // damping rate 
                                                   );   
```

#### How do I change the variables for physics calculation for force-directed graph? ####
To change the stiffness of the spring (edge):  

```c#
m_fdgPhysics.Stiffness = 90.55f;
```

To change the repulsion rate of the node:  

```c#
m_fdgPhysics.Repulsion = 50000.0f;
```

To change the damping:  

```c#
m_fdgPhysics.Damping = 0.7f;
```

Finally you can also set the `Threadshold` to stop the physics iteration at certain point (depends on how you set the threadshold, it will affect the performance of the graph calculation):  

```c#
m_fdgPhysics.Threadshold = 0.1f;
```

This `ForceDirected2D/3D` does the most of the job on the background like figuring the positions of nodes and the edges , and this will be inserted to `Renderer` to calculate the graph to render.  

### Renderer ###

First you need to define your own `Renderer` which inherits `AbstractRenderer`:  

```c#
class Renderer: AbstractRenderer
{
   ...
};
```

You need to implement three methods to make your force-directed graph to render correctly.  

* **Contructor** [ `base(IForceDirected)` ]
  1. You must pass the `IForceDirected` instance, created above, to `AbstractRenderer` constructor when you create your `Renderer`.
* **Clear** [ `void Clear()` ]
  1. Clear any previous drawing to draw new scene
  2. This will be called within `AbstractRenderer::Draw` method.
* **drawEdge** [ `void drawEdge(Edge iEdge, AbstractVector iPosition1, AbstractVector iPosition2)` ]
  1. Draw the given edge according to the given positions
  2. `AbstractVector` will be `FDGVector2` if `ForceDirected2D`, and `FDGVector3` if `ForceDirected3D`
* **drawNode** [ `void drawNode(Node iNode, AbstractVector iPosition)` ]
  1. Draw the given node according to the given position
  2. `AbstractVector` will be `FDGVector2` if `ForceDirected2D`, and `FDGVector3` if `ForceDirected3D`

```c#
class Renderer: AbstractRenderer
{
   public Renderer(IForceDirected iForceDirected): base(iForceDirected)
   {
      // Your initialization to draw
   }

   public override void Clear()
   {
      // Clear previous drawing if needed
      // will be called when AbstractRenderer:Draw is called
   }
   
   protected override void drawEdge(Edge iEdge, AbstractVector iPosition1, AbstractVector iPosition2)
   {
      // Draw the given edge according to given positions
   }

   protected override void drawNode(Node iNode, AbstractVector iPosition)
   {
      // Draw the given node according to given position
   }
};
```

Then create an instance of your `Renderer` with `ForceDirected2D/3D` instance as a parameter:  

```c#
Renderer m_fdgRenderer = new Renderer(m_fdgPhysics);   
```

Finally to draw your `Graph` with the renderer you created above, you simply call `Draw`:  

```c#
float timeStep = 0.05f; // The time passed from previous Draw call in second
m_fdgRenderer.Draw(timeStep);
```

Advanced Usage
------------
### Extendibility ###

#### Expand NodeData ####

You can create your own class which inherits `NodeData` and expand it to hold more variables to use it later like on `Draw` and create the node with your version of `NodeData`:  

```c#
public class MyNodeData: NodeData
{
   public string subType{
      get;set; 
   }
   public MyNodeData(string iSubType):base()
   {
      subType= iSubType;
   }
}

...
MyNodeData data= new MyNodeData("Button");
data.label = "Play";
Node newNode = m_fdgGraph.CreateNode(data);
```

#### Expand EdgeData ####

Similar to the `NodeData`, you can also expand `EdgeData` by creating your own class which inherits `EdgeData`:  

```c#
public class MyEdgeData: EdgeData
{
   public string subType{
      get;set; 
   }
   public MyEdgeData(string iSubType):base()
   {
      subType= iSubType;
   }
}

...
MyEdgeData data= new MyEdgeData("Button");
data.label= "Play";
Edge newEdge = m_fdgGraph.CreateEdge(data);
```

#### Notify on graph structure change ####

You can register a listener class to get notified when the graph structure changed. The listener class must implements the `IGraphEventListener` interface's `GraphChanged` method. You can add the listener as below:  

```c#
IGraphEventListener listener = new MyGraphEventListner();
m_fdgGraph.AddGraphListener(listner); // After this point when graph structure changed, 
                                      //   listener.GraphChanged() method will be called
```

License
-------

[The MIT License](http://opensource.org/licenses/mit-license.php)

Copyright (c) 2013 Woong Gyu La <[juhgiyo@gmail.com](mailto:juhgiyo@gmail.com)>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
