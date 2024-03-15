Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form41
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer
    Dim supp, user As String
    Private Sub Form41_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (Form1.setname = "") Then
            user = ""
        Else
            user = Form1.setname
        End If
        ''''
        fillcombo()
    End Sub
    Private Sub fillcombo()

        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Main_Cat"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Sub_Cat"
        cmd1 = New SqlCommand(SQL, conn)
        dr = cmd1.ExecuteReader
        While (dr.Read())
            ComboBox2.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Supplier"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox3.Items.Add(dr.GetString(0))
            ComboBox4.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '      Form42.clr()
        Form42.Label3.Text = "2"
        Form42.Show()
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
    Private Sub Filldata()
        conn = GetConnect()
        conn.Open()
        '  SQL = "select * from Products where Code ='" + TextBox2.Text + "'"
        SQL = "select ID,Mcat,Scat,Supplier, " +
        " CAST(P_Price as numeric(17,2))P_Price , " +
        " CAST(R_Price as numeric(17,2))R_Price, " +
        " CAST(S_Price as numeric(17,2))S_Price ,Discrip  from Products where Code ='" + TextBox2.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox8.Text = dr.GetValue(0).ToString
            ComboBox1.Text = dr.GetValue(1).ToString
            ComboBox2.Text = dr.GetValue(2).ToString
            ComboBox4.Text = dr.GetValue(3).ToString
            TextBox10.Text = dr.GetValue(4).ToString
            TextBox9.Text = dr.GetValue(5).ToString
            TextBox4.Text = dr.GetValue(6).ToString
            TextBox12.Text = dr.GetValue(6).ToString
            TextBox5.Text = dr.GetValue(7).ToString

        End While

        conn.Close()

        ''''
        If Not ComboBox4.Text = "" Then
            conn = GetConnect()
            conn.Open()

            SQL = "select * from Supplier where Name ='" + ComboBox4.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            dr = cmd.ExecuteReader
            While (dr.Read())
                TextBox11.Text = dr.GetValue(2).ToString
                '   TextBox12.Text = dr.GetValue(2).ToString
            End While
            dr.Close()
            conn.Close()
        End If
    End Sub
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Filldata()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'ID,Mcat,Scat,Supplier,S_Code,Name,Code,Date,Time,P_Price,R_Price,S_Price,Discrip,Login,pflag
        'If ComboBox3.Enabled = False Then
        '    ComboBox4.Text = Val(supp)
        'ElseIf ComboBox3.Enabled = True Then
        '    ComboBox3.Text = Val(supp)
        'End If
        '''''
        Dim d As DateTime = DateTime.Now
        Dim formata As String = "MM/dd/yyyy"

        Dim t As DateTime = DateTime.Now
        Dim formatb As String = "hh:mm:ss tt"
        '''''
        conn = GetConnect()
            conn.Open()
        SQL = "update Products set Mcat = '" + ComboBox1.Text + "',Scat = '" + ComboBox2.Text + "', " &
            " Supplier = '" + ComboBox4.Text + "', Date = '" + d.ToString(formata) + "' ," &
                "Time='" + t.ToString(formatb) + "' ,P_Price = '" + TextBox10.Text + "' , R_Price = '" + TextBox9.Text + "' ,S_price = '" + TextBox4.Text + "' , " &
                "Discrip = '" + TextBox5.Text + "' , Login = '" + user + "' where ID = '" + TextBox8.Text + "'"

        cmd = New SqlCommand(SQL, conn)
            cmd.ExecuteNonQuery()
        conn.Close()
        ''''''       
        updt()
        MsgBox("Updated Successfully !")
        clr()
        ' Catch ex As Exception
        '  End Try
    End Sub
    Private Sub updt()
        conn = GetConnect()
        conn.Open()
        SQL = "update Selling_Price set O_sp = '" + TextBox12.Text + "' ," +
           " N_sp ='" + TextBox4.Text + "' where Code = '" + TextBox2.Text + "'"
        cmd1 = New SqlCommand(SQL, conn)
        cmd1.ExecuteNonQuery()
        conn.Close()
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        conn = GetConnect()
        conn.Open()
        SQL = "delete from Products where ID = '" + TextBox8.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Deleted Successfully !")
        clr()
    End Sub
    Private Sub clr()
        Dim form41 As New Form41
        Me.Hide()
        form41.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        clr()

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        conn = GetConnect()
        SQL = "Update Products set Name = '" + TextBox3.Text + "' where Code = '" + TextBox6.Text + "'"
        conn.Open()
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Product Name Updated !")
        clr()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        conn = GetConnect()
        SQL = "Update Products set Code = '" + TextBox7.Text + "' where Code = '" + TextBox6.Text + "'"
        conn.Open()
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Product Code Updated !")
        clr()
    End Sub

    Private Sub ComboBox3_TextChanged(sender As Object, e As EventArgs) Handles ComboBox3.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Supplier where Name ='" + ComboBox3.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox11.Text = dr.GetValue(2).ToString
        End While
        conn.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form4.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Form42.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
    End Sub

    Private Sub ComboBox4_Click(sender As Object, e As EventArgs) Handles ComboBox4.Click
        'ComboBox4.Visible = False
        'If ComboBox4.Visible = False Then
        '    TextBox11.Text = ""
        '    ComboBox3.Enabled = True
        '    ComboBox3.Focus()
        '    SendKeys.Send("{F4}")
        'End If
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub
End Class