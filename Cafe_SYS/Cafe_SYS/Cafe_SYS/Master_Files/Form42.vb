Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form42
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer
    Public code, pname As String
    Private Sub Form42_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Me.Refresh()
        'ref()
        ''ID,Mcat,Scat,Supplier,S_Code,Name,Code,Date,Time,P_Price,R_Price,S_Price,Discrip,Login,pflag
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select ID,Code,Name,Mcat as Main_Category,Scat as Sub_Category, " +
                                "CAST(P_Price as numeric(17,2))Purchase_Price,CAST(R_Price as numeric(17,2))Retail_Price, " +
                                " CAST(S_Price as numeric(17,2))Sales_Price,Discrip as Discription from Products", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()

        grd()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 50
        BunifuCustomDataGrid1.Columns(1).Width = 100
        BunifuCustomDataGrid1.Columns(2).Width = 250
        BunifuCustomDataGrid1.Columns(3).Width = 200
        BunifuCustomDataGrid1.Columns(4).Width = 200
        BunifuCustomDataGrid1.Columns(5).Width = 100
        BunifuCustomDataGrid1.Columns(6).Width = 100
        BunifuCustomDataGrid1.Columns(7).Width = 100
        BunifuCustomDataGrid1.Columns(8).Width = 250
    End Sub

    Private Sub BunifuCustomDataGrid1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox3.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(1).Value
            code = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(1).Value
            TextBox2.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(2).Value
            pname = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(2).Value
            TextBox4.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(0).Value
        End If
        Me.Hide()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub
    Public Sub clr()
        Dim form42 As New Form42
        Me.Hide()
        form42.Show()

    End Sub
    Private Sub srch()
        If Label3.Text = 2 Then
            Form41.TextBox1.Text = pname
            Form41.TextBox2.Text = code

        End If
        If Label3.Text = "1" Then
            Form3.TextBox5.Text = code
            Form3.TextBox12.Text = code
            Form3.TextBox6.Text = pname
            Form3.TextBox8.Focus()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            conn = GetConnect()
            conn.Open()
            da = New SqlDataAdapter("select ID,Code,Name,Mcat as Main_Category,Scat as Sub_Category, " +
                                "CAST(P_Price as numeric(17,2))Purchase_Price,CAST(R_Price as numeric(17,2))Retail_Price, " +
                                " CAST(S_Price as numeric(17,2))Sales_Price,Discrip as Discription " &
                                    " from Products where Name Like '%" + TextBox1.Text + "%' ", conn)
            dt = New DataTable()
            da.Fill(dt)
            BunifuCustomDataGrid1.DataSource = dt
        Catch ex As Exception
        End Try

        conn.Close()
    End Sub

    Private Sub BunifuCustomDataGrid1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellContentClick

    End Sub
    Private Sub ref()
        Dim form42 As New Form42
        Me.Hide()
        form42.Show()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ref()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        srch()
    End Sub
End Class