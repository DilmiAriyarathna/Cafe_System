Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Control
    Dim caption As String
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Caption from Form_List where Category = 'Settings' and Flag = '1' ", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
        ''
        BunifuCustomDataGrid1.Columns(0).Width = 216
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Caption from Form_List where Category = 'Master Files' and Flag = '1' ", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
        ''
        BunifuCustomDataGrid1.Columns(0).Width = 216
    End Sub

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BunifuCustomDataGrid1.DefaultCellStyle.Font = New Font("Verdena", 9)
        BunifuCustomDataGrid2.DefaultCellStyle.Font = New Font("Verdena", 9)
        ''     
        conn = GetConnect()
            conn.Open()
            da = New SqlDataAdapter("select Caption from Form_List where Flag = '1' Order By Caption", conn)
            dt = New DataTable()
            da.Fill(dt)
            BunifuCustomDataGrid2.DataSource = dt
            conn.Close()
            ''      
            ''
            BunifuCustomDataGrid2.Columns(0).Width = 216
        ''
        Button2_Click(sender, e)
        ''
        Timer1.Start()
        ''
        'TextBox1.Text = "Search Here...."
        'TextBox1.Font = New Font(Font, FontStyle.Italic)
        'TextBox1.ForeColor = Color.Gray
        ''''
        'TextBox2.Text = "Search Here...."
        'TextBox2.Font = New Font(Font, FontStyle.Italic)
        'TextBox2.ForeColor = Color.Gray

        TextBox2.Select()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Caption from Form_List where Category = 'Stock Control' and Flag = '1' ", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
        ''
        BunifuCustomDataGrid1.Columns(0).Width = 216
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Caption from Form_List where Category = 'Sales Data' and Flag = '1'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
        ''
        BunifuCustomDataGrid1.Columns(0).Width = 216
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Caption from Form_List where Category = 'Reports' and Flag = '1'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
        ''
        BunifuCustomDataGrid1.Columns(0).Width = 216
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label14.Text = DateTime.Now.ToString("hh:mm:ss tt")
        Label13.Text = DateTime.Now.ToString("MM/dd/yyyy")
    End Sub

    Private Sub BunifuCustomDataGrid2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid2.CellDoubleClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            caption = BunifuCustomDataGrid2.Rows(e.RowIndex).Cells(0).Value
        End If
        ''
        Formsrc
    End Sub
    Private Sub Formsrc()
        If caption = "Add New Form" Then
            Form9n.Show()
        End If
        ''
        If caption = "Add User Privileges" Then
            Form91n.Show()
        End If
        ''
        If caption = "Daily Stock Count (F/SC)" Then
            Form19.Show()
        End If
        ''
        If caption = "Edit Customer" Then
            Form30.Show()
        End If
        ''
        If caption = "Edit Item" Then
            Form10.Show()
        End If
        ''
        If caption = "Edit Product" Then
            Form41.Show()
        End If
        ''
        If caption = "Edit Product Selling Price" Then
            Form_S.Show()
        End If
        ''
        If caption = "Edit Supplier" Then
            Form14.Show()
        End If
        ''
        If caption = "Edit User" Then
            Form27.Show()
        End If
        ''
        If caption = "GRN" Then
            Form23.Show()
        End If
        ''
        If caption = "Main Category (Item)" Then
            Form7.Show()
        End If
        ''
        If caption = "Main Category (Product)" Then
            Form6.Show()
        End If
        ''
        If caption = "New Bank" Then
            Form15.Show()
        End If
        ''
        If caption = "New Customer" Then
            Form29.Show()
        End If
        ''
        If caption = "New Item" Then
            Form5.Show()
        End If
        ''
        If caption = "New Order" Then
            Form3.Show()
        End If
        ''
        If caption = "New Product" Then
            Form4.Show()
        End If
        ''
        If caption = "New Supplier" Then
            Form12.Show()
        End If
        ''
        If caption = "New User" Then
            Form26.Show()
        End If
        ''
        If caption = "Pending Orders" Then
            Form21.Show()
        End If
        ''
        If caption = "Pending Purchase Order" Then
            Form16.Show()
        End If
        ''
        If caption = "PO_Checking" Then
            Form24.Show()
        End If
        ''
        If caption = "Purchase Order" Then
            Form11.Show()
        End If
        ''
        If caption = "Search Customer" Then
            Form31.Show()
        End If
        ''
        If caption = "Search Item" Then
            Form9.Show()
        End If
        ''
        If caption = "Search Product" Then
            Form42.Show()
        End If
        ''
        If caption = "Search Supplier" Then
            Form13.Show()
        End If
        ''
        If caption = "Stores" Then
            Form28.Show()
        End If
        ''
        If caption = "" Then
            Form9n.Show()
        End If
        ''
        If caption = "Sub Category (Item)" Then
            Form71.Show()
        End If
        ''
        If caption = "Sub Category (Product)" Then
            Form61.Show()
        End If
        ''  
        If caption = "Item Return" Then
            Form33.Show()
        End If
        ''
        If caption = "New Employee" Then
            Form38.Show()
        End If
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        'TextBox1.Text = ""
        'TextBox1.Font = New Font(Font, FontStyle.Regular)
        'TextBox1.ForeColor = Color.Black
    End Sub

    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
        'TextBox2.Text = ""
        'TextBox2.Font = New Font(Font, FontStyle.Regular)
        'TextBox2.ForeColor = Color.Black
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Caption from Form_List where Flag = '1' and Caption like '%" + TextBox2.Text + "%' Order By Caption", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid2.DataSource = dt
        conn.Close()
    End Sub

    Private Sub BunifuCustomDataGrid1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellDoubleClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            caption = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(0).Value
        End If
        ''
        Formsrc()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim result As DialogResult = MsgBox("Do you want exit ?", vbYesNo, "The Garden Cafe")
        If (result = vbYes) Then
            Application.Exit()
        End If
    End Sub
End Class