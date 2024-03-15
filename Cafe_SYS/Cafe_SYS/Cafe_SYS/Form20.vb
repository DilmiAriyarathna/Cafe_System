Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Public Class Form20
    '  Public conn As New SqlConnection
    'Dim SQL As String
    'Dim cmd, cmd1 As SqlCommand
    'Dim da As New SqlDataAdapter
    'Dim ds As New DataSet
    'Dim dr As SqlDataReader
    'Dim dt As DataTable
    'Dim dv As DataView
    Dim x, tmpcnt, cnt As Integer
    Dim user, mcat, scat, year, month As String
    Dim gtot As Double
    Dim total, dis1, dis2 As Double
    Dim datatbl As DataTable
    Dim table As New DataTable("Table")

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        For Each row As DataGridViewRow In BunifuCustomDataGrid1.Rows
            conn = GetConnect()
            conn.Open()
            SQL = "insert into Table_2(id,name,address,age,tot) values (@id,@name,@address,@age,@tot)"
            cmd = New SqlCommand(SQL, conn)
            cmd.Parameters.AddWithValue("@id", row.Cells("Id").Value)
            cmd.Parameters.AddWithValue("@name", row.Cells("Name").Value)
            cmd.Parameters.AddWithValue("@address", row.Cells("Address").Value)
            cmd.Parameters.AddWithValue("@age", row.Cells("Age").Value)
            cmd.Parameters.AddWithValue("@tot", TextBox2.Text)
            cmd.ExecuteNonQuery()
            conn.Close()
        Next
        MsgBox("Insert !")
    End Sub

    'Private Sub insrt()
    '    conn = GetConnect()
    '    conn.Open()
    '    SQL = "select 8 from Table_2"
    '    cmd = New SqlCommand(SQL, conn)
    '    With cmd
    '        .Addnew

    '    End With
    '    conn.Close()
    'End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        table.Rows.Add(TextBox1.Text, TextBox9.Text, TextBox10.Text,
                        TextBox11.Text)
        BunifuCustomDataGrid1.DataSource = table
    End Sub



    Private Sub Form20_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        table.Columns.Add("Id", Type.GetType("System.Int32"))
        table.Columns.Add("Name", Type.GetType("System.String"))
        table.Columns.Add("Address", Type.GetType("System.String"))
        table.Columns.Add("Age", Type.GetType("System.Int32"))

        BunifuCustomDataGrid1.DataSource = table
    End Sub




    Private Sub History_addGrid(name, age, telephone)
        ''  DataGridViewRow newr = New DataGridViewRow 

    End Sub

End Class