Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Public Class Form21

    Dim d As DateTime = DateTime.Now
    Dim formata As String = "MM/dd/yyyy"

    Dim t As DateTime = DateTime.Now
    Dim formatb As String = "hh:mm:ss tt"
    Private Sub Form21_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select CAST(Date AS DATE)Date,CAST(Time AS Time)Time,Order_No, " &
                                "Cashier,CAST(Total as numeric(17,2))Total from Order_List_qq where Date = '" + d.ToString(formata) + "'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()

        grd()
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 110
        BunifuCustomDataGrid1.Columns(1).Width = 110
        BunifuCustomDataGrid1.Columns(2).Width = 110
        BunifuCustomDataGrid1.Columns(3).Width = 110
        BunifuCustomDataGrid1.Columns(4).Width = 110
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Not TextBox1.Text = "" Then
            conn = GetConnect()
            conn.Open()
            da = New SqlDataAdapter("select CAST(Date AS DATE)Date,CAST(Time AS Time)Time,Order_No, " &
                                    "Cashier,CAST(Total as numeric(17,2))Total from Order_List_qq where Order_No like '%" + TextBox1.Text + "%'", conn)
            dt = New DataTable()
            da.Fill(dt)
            BunifuCustomDataGrid1.DataSource = dt
            conn.Close()
        ElseIf TextBox1.Text = "" Then
            conn = GetConnect()
            conn.Open()
            da = New SqlDataAdapter("select CAST(Date AS DATE)Date,CAST(Time AS Time)Time,Order_No, " &
                                    "Cashier,CAST(Total as numeric(17,2))Total from Order_List_qq where Date = '" + d.ToString(formata) + "'", conn)
            dt = New DataTable()
            da.Fill(dt)
            BunifuCustomDataGrid1.DataSource = dt
            conn.Close()
        End If
    End Sub
    Private Sub clr()
        Dim form21 As New Form21
        Me.Hide()
        form21.Show()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'clr()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        clr()
    End Sub

    Private Sub BunifuCustomDataGrid1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox2.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(2).Value
            TextBox3.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(2).Value
            '//  TextBox4.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(2).Value
        End If
        Me.Hide()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If TextBox6.Text = "1" Then
            ''Form11.TextBox13.Text = TextBox2.Text
            ''Form11.TextBox13.Focus()
            ''Form11.Button12.Enabled = True
            ''Form11.Button11.Enabled = False
            ''Form11.Button10.Enabled = False
        End If
    End Sub
End Class