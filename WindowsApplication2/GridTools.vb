Module GridTools

    Public Sub prettyPrintGrid(grid As Grid)

        'TBD: Do sort

        Dim lastY As Integer= 0
        for Each node As Node In grid.getNode()

           
            If lastY < node.coorY() Then
                 lastY= node.coorY()
                 Debug.WriteLine("")
            End If

            If node.isWall Then 
                Debug.Write("*")
            Else 
                Debug.Write(".")
            End If
        Next node

        Debug.WriteLine("")

    End Sub

End Module
