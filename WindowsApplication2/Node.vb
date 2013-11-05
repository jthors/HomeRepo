Public Class Node

    Private wallNode As Boolean
    Private definedPath As Boolean
    Private path As Boolean
    Private x As Integer
    Private y As Integer

    Public Sub New(x As Integer, y As Integer, isWall As Boolean, isDefinedPath As Boolean)
        Me.wallNode= isWall
        Me.definedPath= isDefinedPath
        Me.x= x
        Me.y= y
    End Sub

    Public ReadOnly Property isDefinedPath As Boolean
       Get
          Return Me.definedPath
       End Get
    End Property

    Public ReadOnly Property IsWall() As Boolean
       Get
          Return Me.wallNode
       End Get
    End Property

    Friend ReadOnly Property coorY as Integer
        Get
            return y
        End Get
    End Property

    Friend ReadOnly Property coorX as Integer
        Get
            return x
        End Get
    End Property
End Class


Public Class Grid

    Private maxX As Integer
    Private maxY As Integer
    Private gridNodes As ICollection(Of Node)

    'TBD what sets/lists are available
    Sub New
        Me.gridNodes= New List(Of Node)
    End Sub

    Public ReadOnly Property getNodes() As IEnumerable
        Get
            Return Me.gridNodes
        End Get
    End Property


    public Sub initializeGrid(x As Integer, y As Integer) 
        maxX= x
        maxY= y
        For i As Integer = 0 To maxY
            For j As Integer = 0 To maxX
                gridNodes.Add(New Node(j, i, j = 0 Or j = x Or i = 0 or i = y, False))
            Next
        Next i

    End Sub

    Private Function getNode(startX As Integer, startY As Integer) As Node 
        
        For Each node In gridNodes
            If(node.coorX = startX And node.coorY = startY) Then Return node
        Next
        Return Nothing

    End Function 

    Private Function getNodeNorth(ByVal currentNode As Node) As Node
        Dim nodeX= currentNode.coorX
        Dim nodeY= currentNode.coorY+1

        Return getNode(nodeX, nodeY)

    End Function

    Private Function getNodeSouth(ByVal currentNode As Node) As Node
        Dim nodeX= currentNode.coorX
        Dim nodeY= currentNode.coorY-1

        Return getNode(nodeX, nodeY)

    End Function

    Private Function getNodeWest(ByVal currentNode As Node) As Node
        Dim nodeX= currentNode.coorX-1
        Dim nodeY= currentNode.coorY

        Return getNode(nodeX, nodeY)

    End Function

    Private Function getNodeEast(ByVal currentNode As Node) As Node
        Dim nodeX= currentNode.coorX+1
        Dim nodeY= currentNode.coorY

        Return getNode(nodeX, nodeY)

    End Function

    Private Function canNorth(Node As Node) As Boolean
        Return False
    End Function

    Private Function canSouth(Node As Node) As Boolean
        Return False
    End Function

    Private Function canEast(Node As Node) As Boolean
        Return False
    End Function

    Private Function canWest(Node As Node) As Boolean
        Return False
    End Function

    Function validatePath(startX As Integer, startY As Integer, text As String) As Boolean

       Dim startNode= getNode(startX, startY)

       If startNode Is Nothing Then Return False

       Return startNode.IsWall And validatePath(startNode, text) 

    End Function

    Private Function validatePath(startNode As Node, ByVal text As String) As Boolean

        Dim currentPath As String= ""
        Dim currentNode= startNode
        Dim visitedNodes As New List(Of Node)
        visitedNodes.Add(currentNode)
        

        While text.Length > 0 
            Dim move As String= text.Substring(0,1)
            'TBD: Does this append?
            currentPath+= move
            text= text.Substring(1)

            Select Case move
                Case "N"
                    Debug.WriteLine("N")
                    currentNode= getNodeNorth(currentNode)
                Case "S"
                    Debug.WriteLine("S")
                    currentNode= getNodeSouth(currentNode)
                Case "E"
                    Debug.WriteLine("E")
                    currentNode= getNodeEast(currentNode)
                Case "W"
                    Debug.WriteLine("W")
                    currentNode= getNodeWest(currentNode)
                Case Else
                    Return False
            End Select

            If currentNode Is Nothing Then 
                MsgBox("Path step out of bounds")
                Return False
            End If

            If currentNode.IsWall Then 
                MsgBox("Path touches bounds")
                Return False
            End If

            If visitedNodes.Contains(currentNode) Then
                MsgBox("Cycle detected")
                Return False
            End If
            visitedNodes.Add(currentNode)

        End While

        Return True
    End Function


End Class