Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form9
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer
    Dim user As String

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'clr()
        Display()
        grd()
        FillCombo()

        'TextBox7.Text = TextBox5.Text
        'TextBox7.SelectionStart = 0
        'TextBox7.SelectionLength = 13

        'TextBox4.Text = TextBox7.SelectedText

        'TextBox7.SelectionStart = 13
        'TextBox7.SelectionLength = 26

        'TextBox8.Text = TextBox7.SelectedText

        'TextBox9.Text = TextBox4.Text & vbNewLine & TextBox8.Text


    End Sub

    Private Sub Display()
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT TOP 1000 ID,Name,Code,CAST(P_Price as numeric(17,2))Purchase_Price, " &
                    "CAST(R_Price as numeric(17,2))Retail_Price, " &
                "CAST(S_Price as numeric(17,2))Sales_Price, " &
                    "CAST(W_Price as numeric(17,2))Wholesale_Price, " &
                "pack_Qty as PCK_Size, " &
                "CAST(PR_price as numeric(17,2))PCK_Retail_Price, " &
                "CAST(PW_price as numeric(17,2))PCK_wholesale_Price,Supplier  " &
                " FROM Item"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 85
        BunifuCustomDataGrid1.Columns(1).Width = 300
        BunifuCustomDataGrid1.Columns(2).Width = 100
        BunifuCustomDataGrid1.Columns(3).Width = 100
        BunifuCustomDataGrid1.Columns(4).Width = 100
        BunifuCustomDataGrid1.Columns(5).Width = 100
        BunifuCustomDataGrid1.Columns(6).Width = 100
        BunifuCustomDataGrid1.Columns(7).Width = 100
        BunifuCustomDataGrid1.Columns(8).Width = 150
        BunifuCustomDataGrid1.Columns(9).Width = 150
        BunifuCustomDataGrid1.Columns(10).Width = 300
        '   BunifuCustomDataGrid1.Columns(11).Width = 100
        '// BunifuCustomDataGrid1.Columns(7).Width = 100
    End Sub
    Private Sub src1()
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT TOP 1000 ID,Name,Code,CAST(P_Price as numeric(17,2))Purchase_Price, " &
                    "CAST(R_Price as numeric(17,2))Retail_Price, " &
                "CAST(S_Price as numeric(17,2))Sales_Price, " &
                    "CAST(W_Price as numeric(17,2))Wholesale_Price, " &
                "pack_Qty as PCK_Size, " &
                "CAST(PR_price as numeric(17,2))PCK_Retail_Price, " &
                "CAST(PW_price as numeric(17,2))PCK_wholesale_Price,Supplier  " &
                " FROM Item where Mcat='" + TextBox15.Text + "' and Scat='" + TextBox16.Text + "' and " &
                " Name like '%" + TextBox1.Text + "%'"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub src2()
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT TOP 1000 ID,Name,Code,CAST(P_Price as numeric(17,2))Purchase_Price, " &
                    "CAST(R_Price as numeric(17,2))Retail_Price, " &
                "CAST(S_Price as numeric(17,2))Sales_Price, " &
                    "CAST(W_Price as numeric(17,2))Wholesale_Price, " &
                "pack_Qty as PCK_Size, " &
                "CAST(PR_price as numeric(17,2))PCK_Retail_Price, " &
                "CAST(PW_price as numeric(17,2))PCK_wholesale_Price,Supplier  " &
                " FROM Item where Mcat='" + TextBox15.Text + "' and Scat='" + TextBox16.Text + "' and " &
                " Code like '%" + TextBox1.Text + "%'"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub src3()
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT TOP 1000 ID,Name,Code,CAST(P_Price as numeric(17,2))Purchase_Price, " &
                    "CAST(R_Price as numeric(17,2))Retail_Price, " &
                "CAST(S_Price as numeric(17,2))Sales_Price, " &
                    "CAST(W_Price as numeric(17,2))Wholesale_Price, " &
                "pack_Qty as PCK_Size, " &
                "CAST(PR_price as numeric(17,2))PCK_Retail_Price, " &
                "CAST(PW_price as numeric(17,2))PCK_wholesale_Price,Supplier  " &
                " FROM Item where Mcat='" + TextBox15.Text + "'  and " &
                " Name like '%" + TextBox1.Text + "%'"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub src4()
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT TOP 1000 ID,Name,Code,CAST(P_Price as numeric(17,2))Purchase_Price, " &
                    "CAST(R_Price as numeric(17,2))Retail_Price, " &
                "CAST(S_Price as numeric(17,2))Sales_Price, " &
                    "CAST(W_Price as numeric(17,2))Wholesale_Price, " &
                "pack_Qty as PCK_Size, " &
                "CAST(PR_price as numeric(17,2))PCK_Retail_Price, " &
                "CAST(PW_price as numeric(17,2))PCK_wholesale_Price,Supplier  " &
                " FROM Item where Mcat='" + TextBox15.Text + "'  and " &
                " Code like '%" + TextBox1.Text + "%'"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub src5()
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT TOP 1000 ID,Name,Code,CAST(P_Price as numeric(17,2))Purchase_Price, " &
                    "CAST(R_Price as numeric(17,2))Retail_Price, " &
                "CAST(S_Price as numeric(17,2))Sales_Price, " &
                    "CAST(W_Price as numeric(17,2))Wholesale_Price, " &
                "pack_Qty as PCK_Size, " &
                "CAST(PR_price as numeric(17,2))PCK_Retail_Price, " &
                "CAST(PW_price as numeric(17,2))PCK_wholesale_Price,Supplier  " &
                " FROM Item where Scat='" + TextBox16.Text + "' and " &
                " Name like '%" + TextBox1.Text + "%'"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub src6()
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT TOP 1000 ID,Name,Code,CAST(P_Price as numeric(17,2))Purchase_Price, " &
                    "CAST(R_Price as numeric(17,2))Retail_Price, " &
                "CAST(S_Price as numeric(17,2))Sales_Price, " &
                    "CAST(W_Price as numeric(17,2))Wholesale_Price, " &
                "pack_Qty as PCK_Size, " &
                "CAST(PR_price as numeric(17,2))PCK_Retail_Price, " &
                "CAST(PW_price as numeric(17,2))PCK_wholesale_Price,Supplier  " &
                " FROM Item where  Scat='" + TextBox16.Text + "' and " &
                " Code like '%" + TextBox1.Text + "%'"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub src7()
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT TOP 1000 ID,Name,Code,CAST(P_Price as numeric(17,2))Purchase_Price, " &
                    "CAST(R_Price as numeric(17,2))Retail_Price, " &
                "CAST(S_Price as numeric(17,2))Sales_Price, " &
                    "CAST(W_Price as numeric(17,2))Wholesale_Price, " &
                "pack_Qty as PCK_Size, " &
                "CAST(PR_price as numeric(17,2))PCK_Retail_Price, " &
                "CAST(PW_price as numeric(17,2))PCK_wholesale_Price,Supplier  " &
                " FROM Item where " &
                " Name like '%" + TextBox1.Text + "%'"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub src8()
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT TOP 1000 ID,Name,Code,CAST(P_Price as numeric(17,2))Purchase_Price, " &
                    "CAST(R_Price as numeric(17,2))Retail_Price, " &
                "CAST(S_Price as numeric(17,2))Sales_Price, " &
                    "CAST(W_Price as numeric(17,2))Wholesale_Price, " &
                "pack_Qty as PCK_Size, " &
                "CAST(PR_price as numeric(17,2))PCK_Retail_Price, " &
                "CAST(PW_price as numeric(17,2))PCK_wholesale_Price,Supplier  " &
                " FROM Item where " &
                " Code like '%" + TextBox1.Text + "%'"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If Not ComboBox1.Text = "" Then
            '''''''''''''''''''''''''''' Mcat <> ""
            If Not ComboBox2.Text = "" Then
                ''''''''''''''''''''''''''''' scat <>""
                If ComboBox3.Text = "Discription" Then
                    src1()
                ElseIf ComboBox3.Text = "Code" Then
                    src2()
                End If

            ElseIf ComboBox2.Text = "" Then
                ''''''''''''''''''''''''''''''mcat<>"" and scat = ""
                If ComboBox3.Text = "Discription" Then
                    src3()
                ElseIf ComboBox3.Text = "Code" Then
                    src4()
                End If

            End If  'scat <>""

        ElseIf ComboBox1.Text = "" Then
            ''''''''''''''''''''''''''''''''''' mcat=""
            If Not ComboBox2.Text = "" Then
                ''''''''''''''''''''''''''''' scat <>""

                If ComboBox3.Text = "Discription" Then
                    src5()
                ElseIf ComboBox3.Text = "Code" Then
                    src6()
                End If

            ElseIf ComboBox2.Text = "" Then
                '''''''''''''''''''''''''''''''''''''scat = ""
                If ComboBox3.Text = "Discription" Then
                    src7()
                ElseIf ComboBox3.Text = "Code" Then
                    src8()
                End If

            End If
        End If 'maincat <>""
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Main_Cat2 where Name ='" + ComboBox1.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox15.Text = dr.GetValue(1).ToString
        End While
        conn.Close()
    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Sub_Cat2 where Name ='" + ComboBox2.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox16.Text = dr.GetValue(1).ToString
        End While
        conn.Close()
    End Sub
    Private Sub FillCombo()
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Main_Cat2"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        '''''''''''''''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Sub_Cat2"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox2.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Not ComboBox1.Text = "" Then
            If Not ComboBox2.Text = "" Then
                ' with mcat and scat
                wcat()
            ElseIf ComboBox2.Text = "" Then
                ' with mcat
                mcat()
            End If
        ElseIf ComboBox1.Text = "" Then
            If Not ComboBox2.Text = "" Then
                ' with scat
                scat()
            ElseIf ComboBox2.Text = "" Then
                MsgBox("Invalid Category !")
            End If
        End If
    End Sub
    Private Sub clr()
        Dim form9 As New Form9
        Me.Hide()
        form9.Show()
    End Sub
    Private Sub mcat()
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT TOP 1000 ID,Name,Code,CAST(P_Price as numeric(17,2))Purchase_Price, " &
                    "CAST(R_Price as numeric(17,2))Retail_Price, " &
                "CAST(S_Price as numeric(17,2))Sales_Price, " &
                    "CAST(W_Price as numeric(17,2))Wholesale_Price, " &
                "pack_Qty as PCK_Size, " &
                "CAST(PR_price as numeric(17,2))PCK_Retail_Price, " &
                "CAST(PW_price as numeric(17,2))PCK_wholesale_Price,Supplier  " &
                " FROM Item where Mcat='" + TextBox15.Text + "'  "

        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub scat()
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT TOP 1000 ID,Name,Code,CAST(P_Price as numeric(17,2))Purchase_Price, " &
                    "CAST(R_Price as numeric(17,2))Retail_Price, " &
                "CAST(S_Price as numeric(17,2))Sales_Price, " &
                    "CAST(W_Price as numeric(17,2))Wholesale_Price, " &
                "pack_Qty as PCK_Size, " &
                "CAST(PR_price as numeric(17,2))PCK_Retail_Price, " &
                "CAST(PW_price as numeric(17,2))PCK_wholesale_Price,Supplier  " &
                " FROM Item where Scat='" + TextBox16.Text + "'  "

        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        clr()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub BunifuCustomDataGrid1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox5.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(1).Value
            TextBox4.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(2).Value
            TextBox7.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(2).Value
        End If
        Me.Hide()
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        If TextBox6.Text = "1" Then
            Form10.TextBox1.Text = TextBox5.Text
            Form10.TextBox2.Text = TextBox4.Text
            Form10.Label18.Text = TextBox4.Text
            Form10.TextBox3.Focus()
        End If

        If TextBox6.Text = "2" Then
            Form11.TextBox4.Text = TextBox5.Text
            Form11.TextBox5.Text = TextBox4.Text
            Form11.Label18.Text = TextBox4.Text
            Form11.TextBox7.Focus()
        End If
    End Sub

    Private Sub BunifuCustomDataGrid1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellContentClick

    End Sub

    Private Sub wcat()
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT TOP 1000 ID,Name,Code,CAST(P_Price as numeric(17,2))Purchase_Price, " &
                    "CAST(R_Price as numeric(17,2))Retail_Price, " &
                "CAST(S_Price as numeric(17,2))Sales_Price, " &
                    "CAST(W_Price as numeric(17,2))Wholesale_Price, " &
                "pack_Qty as PCK_Size, " &
                "CAST(PR_price as numeric(17,2))PCK_Retail_Price, " &
                "CAST(PW_price as numeric(17,2))PCK_wholesale_Price,Supplier  " &
                " FROM Item where Mcat='" + TextBox15.Text + "' and Scat ='" + TextBox16.Text + "' "

        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

End Class