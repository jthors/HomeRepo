Imports System.Data.SqlServerCe
Imports System.Data.SqlClient

Public Class Form1
    Private connection As SqlCeConnection
    'Private engine As SqlCeEngine()

    Private Sub CreateConnection()  

        Dim uri As New Uri(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        Dim path As String= System.IO.Path.GetDirectoryName(Uri.UnescapeDataString(uri.AbsolutePath))
        connection = New SqlCeConnection("Data Source="+path+"\Books3.sdf")
        connection.Open()
        'engine.CreateDatabase()

    End Sub

    Private Sub CloseConnection()
        Try
            connection.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles Button1.Click

        DataGridView1.EndEdit()
        
        Try
            CreateConnection()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Dim cmd As New SqlCeCommand("Select * from Books")
         Dim cmdBuilder = new SqlCeCommandBuilder()
        Try
           
            cmd.Connection= connection
            Dim adp As New SqlCeDataAdapter(cmd)
            cmdBuilder.DataAdapter= adp
    
            adp.SelectCommand= cmd
            adp.UpdateCommand= cmdBuilder.GetUpdateCommand(true)
            'adp.InsertCommand= cmdBuilder.GetInsertCommand(true)
            Dim ds As DataTable = DataGridView1.DataSource
            adp.Update(ds)
            
        Catch ex As Exception

            MsgBox(ex.Message)
        End Try

        Form1_Load(Me, EventArgs.Empty)

        Try
            CloseConnection()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim DS As New DataTable

        Try
            CreateConnection()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try
            Dim cmd As New SqlCeCommand("Select * from Books")
            cmd.Connection= connection
            Dim adp As New SqlCeDataAdapter(cmd)
     

       
            adp.Fill(DS)
            'Dim IdCol As DataColumn = DS.Columns("Id")
            'DS.PrimaryKey = New DataColumn(){IdCol}

            DataGridView1.AutoGenerateColumns = False
            DataGridView1.DataSource= DS
            DataGridView1.ClearSelection()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        DataGridView1.Refresh()

        Try
            CloseConnection()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub Button2_Click( sender As System.Object,  e As System.EventArgs) Handles Button2.Click

        If DataGridView1.AreAllCellsSelected(False) Then Return

        Try
            CreateConnection()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try
            Dim row = DataGridView1.SelectedRows.Item(0)
            Dim Id As Integer= CInt(row.Cells.Item(0).Value)

            Dim cmd As New SqlCeCommand("DELETE FROM Books WHERE Id = @Id", connection)
            cmd.Connection= connection
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id
            
            cmd.ExecuteNonQuery()

        Catch ex As Exception
             MsgBox(ex.Message)
        End Try

        Form1_Load(Me, EventArgs.Empty)

        Try
            CloseConnection()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button3_Click( sender As System.Object,  e As System.EventArgs) Handles Button3.Click

        Dim grid As New Grid

        grid.initializeGrid(20, 20)
        If Not grid.validatePath(0, 3, txtDefinedPath.Text) Then
            MsgBox("Invalid Path!")
            Return
        End If

        GridTools.prettyPrintGrid(grid)
    End Sub

End Class
