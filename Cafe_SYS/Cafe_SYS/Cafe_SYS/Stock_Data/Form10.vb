Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form10
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer
    Dim user, mcat, scat, supp As String

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form5.Show()
    End Sub
    Private Sub FillCombo()
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Main_Cat2"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
            ComboBox5.Items.Add(dr.GetString(0))
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
            ComboBox6.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''''''''''''''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Supplier"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox3.Items.Add(dr.GetString(0))
            ComboBox7.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form9.TextBox6.Text = "1"
        Form9.Show()
    End Sub

    Private Sub Label18_TextChanged(sender As Object, e As EventArgs) Handles Label18.TextChanged
        filldata()
    End Sub
    Private Sub filldata()
        conn = GetConnect()
        conn.Open()
        SQL = "select TOP 1000 ID,Main_cat,Sub_cat,Supplier,S_Code,Descrip,Code,Date,Time, " &
        " CAST(P_Price as numeric(17,2))P_Price,CAST(R_Price as numeric(17,2))R_Price, " &
        " CAST(S_Price as numeric(17,2))S_Price,CAST(W_Price as numeric(17,2))W_Price, " &
        " Weight,pack_Qty, CAST(PW_Price as numeric(17,2))PW_price,CAST(PR_Price as numeric(17,2))PR_price, " &
        " spw,wflag,Login,pflag from All_q1 where Code = '" + Label18.Text + "'"

        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        If (dr.Read()) Then
            TextBox6.Text = dr.GetValue(0).ToString     'id
            ComboBox5.Text = dr.GetValue(1).ToString    'main
            ComboBox6.Text = dr.GetValue(2).ToString    'sub
            ComboBox7.Text = dr.GetValue(3).ToString    'supp
            TextBox21.Text = dr.GetValue(4).ToString    'supcode

            TextBox3.Text = dr.GetValue(9).ToString     'purchase
            TextBox13.Text = dr.GetValue(10).ToString   'retail
            TextBox5.Text = dr.GetValue(11).ToString    'sales
            TextBox4.Text = dr.GetValue(12).ToString    'wholesale
            TextBox7.Text = dr.GetValue(13).ToString    'weight
            TextBox8.Text = dr.GetValue(14).ToString    'pack
            TextBox14.Text = dr.GetValue(15).ToString   'pw
            TextBox10.Text = dr.GetValue(16).ToString   'pr
            'TextBox21.Text = dr.GetValue(4).ToString
            'TextBox21.Text = dr.GetValue(4).ToString
            'TextBox21.Text = dr.GetValue(4).ToString
            'TextBox21.Text = dr.GetValue(4).ToString
        End If
        conn.Close()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If Not ComboBox5.Text = "" Then
            conn = GetConnect()
            conn.Open()
            SQL = "select * from Main_Cat2 where Name ='" + ComboBox5.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            dr = cmd.ExecuteReader
            While (dr.Read())
                TextBox15.Text = dr.GetValue(1).ToString
            End While
            dr.Close()
            conn.Close()
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If Not ComboBox6.Text = "" Then
            conn = GetConnect()
            conn.Open()
            SQL = "select * from Sub_Cat2 where Name ='" + ComboBox6.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            dr = cmd.ExecuteReader
            While (dr.Read())
                TextBox16.Text = dr.GetValue(1).ToString
            End While
            dr.Close()
            conn.Close()
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If Not ComboBox7.Text = "" Then
            conn = GetConnect()
            conn.Open()

            SQL = "select * from Supplier where Name ='" + ComboBox7.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            dr = cmd.ExecuteReader
            While (dr.Read())
                TextBox17.Text = dr.GetValue(2).ToString
                '   TextBox12.Text = dr.GetValue(2).ToString
            End While
            dr.Close()
            conn.Close()
        End If
    End Sub

    Private Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If (Form1.setname = "") Then
            user = ""
        Else
            user = Form1.setname
        End If

        FillCombo()
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
        dr.Close()
        conn.Close()
    End Sub

    Private Sub ComboBox5_TextChanged(sender As Object, e As EventArgs) Handles ComboBox5.TextChanged

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
        dr.Close()
        conn.Close()
    End Sub

    Private Sub ComboBox6_TextChanged(sender As Object, e As EventArgs) Handles ComboBox6.TextChanged

    End Sub

    Private Sub ComboBox3_TextChanged(sender As Object, e As EventArgs) Handles ComboBox3.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Supplier where Name ='" + ComboBox3.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox17.Text = dr.GetValue(2).ToString
            ' TextBox12.Text = dr.GetValue(2).ToString
        End While
        dr.Close()
        conn.Close()
    End Sub

    Private Sub ComboBox7_TextChanged(sender As Object, e As EventArgs) Handles ComboBox7.TextChanged

    End Sub

    Private Sub ComboBox5_Click(sender As Object, e As EventArgs) Handles ComboBox5.Click
        ' TextBox15.Text = ""
        ComboBox5.Visible = False
        If ComboBox5.Visible = False Then
            TextBox15.Text = ""
            ComboBox1.Enabled = True
            ComboBox1.Focus()
            SendKeys.Send("{F4}")
        End If

    End Sub

    Private Sub ComboBox6_Click(sender As Object, e As EventArgs) Handles ComboBox6.Click
        ComboBox6.Visible = False
        If ComboBox6.Visible = False Then
            TextBox16.Text = ""
            ComboBox2.Enabled = True
            ComboBox2.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub ComboBox7_Click(sender As Object, e As EventArgs) Handles ComboBox7.Click
        ComboBox7.Visible = False
        If ComboBox7.Visible = False Then
            TextBox17.Text = ""
            ComboBox3.Enabled = True
            ComboBox3.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

        ''''''''''''''''''''''''''''''''''''''''''''//check Sub category fill or not
        If TextBox15.Text = "" Then
            MsgBox("Invalid Main Category !")
            If (ComboBox1.Enabled = False) Then
                ComboBox1.Enabled = True
                ComboBox1.Focus()
                SendKeys.Send("{F4}")
            Else
                ComboBox1.Focus()
                SendKeys.Send("{F4}")
            End If
            Exit Sub
        End If

        ''''''''''''''''''''''''''''''''''''''''''''//check Sub category fill or not
        If TextBox16.Text = "" Then
            MsgBox("Invalid Sub Category !")
            If (ComboBox2.Enabled = False) Then
                ComboBox2.Enabled = True
                ComboBox2.Focus()
                SendKeys.Send("{F4}")
            Else
                ComboBox2.Focus()
                SendKeys.Send("{F4}")
            End If
            Exit Sub
        End If

        ''''''''''''''''''''''''''''''''''''''''''''//check Supp  fill or not
        If TextBox16.Text = "" Then
            MsgBox("Invalid Supplier !")
            If (ComboBox3.Enabled = False) Then
                ComboBox3.Enabled = True
                ComboBox3.Focus()
                SendKeys.Send("{F4}")
            Else
                ComboBox3.Focus()
                SendKeys.Send("{F4}")
            End If
            Exit Sub
        End If

        '''''''''''''''''purchase price cannot be empty
        If TextBox3.Text = "" Then
            MsgBox("Invalid Purchase Price !")
            TextBox3.Focus()
            Exit Sub
        End If
        '''''''''''''''''Retail price cannot be empty
        If TextBox13.Text = "" Then
            MsgBox("Invalid Retail Price !")
            TextBox13.Focus()
            Exit Sub
        End If
        '''''''''''''''' Sales price cannot be empty
        If TextBox5.Text = "" Then
            MsgBox("Invalid Sales Price !")
            TextBox5.Focus()
            Exit Sub
        End If
        ''''''''''''''''whaloseles price cannot be empty
        If TextBox4.Text = "" Then
            MsgBox("Invalid WholeSale Price !")
            TextBox4.Focus()
            Exit Sub
        End If
        ''''''''''''''''''''''
        mcat = Val(TextBox15.Text)
        scat = Val(TextBox16.Text)
        If ComboBox3.Enabled = False Then
            supp = Val(ComboBox7.Text)
        ElseIf ComboBox3.Enabled = True Then
            supp = Val(ComboBox3.Text)
        End If
        ''''

        'Date time///////////
        Dim d As DateTime = DateTime.Now
        Dim formata As String = "MM/dd/yyyy"

        Dim t As DateTime = DateTime.Now
        Dim formatb As String = "hh:mm:ss tt"


        ''''''''''''''''''''''''''''''''''''''''''''''''''Code start ---//UPDATE\\---
        conn = GetConnect()
        conn.Open()
        SQL = "update item set Mcat = '" + mcat + "' , Scat = '" + scat + "' , Supplier = '" + supp + "' , " &
            "S_Code = '" + Label17.Text + "' , Date = '" + d.ToString(formata) + "' , Time ='" + t.ToString(formatb) + "', " &
            " P_Price='" + TextBox3.Text + "',R_Price='" + TextBox13.Text + "',S_Price='" + TextBox5.Text + "', " &
            " W_Price = '" + TextBox4.Text + "' , Weight='" + TextBox7.Text + "' , pack_Qty = '" + TextBox8.Text + "', " &
            "  PR_price='" + TextBox14.Text + "',PW_price='" + TextBox10.Text + "', Login = '" + user + "' " &
            " where  Name = '" + TextBox1.Text + "' and Code = '" + TextBox2.Text + "' and ID = '" + TextBox6.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Successfully updated !")
        clr()

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        conn = GetConnect()
        conn.Open()
        SQL = "update Item set Name ='" + TextBox20.Text + "' where Name = '" + TextBox1.Text + "' and Code = '" + TextBox2.Text + "' "
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Successfully Updated !")
        clr()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        conn = GetConnect()
        conn.Open()
        SQL = "update Item set Code ='" + TextBox19.Text + "' where Name = '" + TextBox1.Text + "' and Code = '" + TextBox2.Text + "' "
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Successfully Updated !")
        clr()
    End Sub
    Private Sub clr()
        Dim form10 As New Form10
        Me.Hide()
        form10.Show()
    End Sub

    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox10.Text = Val(TextBox8.Text) * Val(TextBox13.Text)
            TextBox14.Text = Val(TextBox8.Text) * Val(TextBox4.Text)
            '    TextBox10.Focus()
        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox13.Focus()
        End If
    End Sub

    Private Sub TextBox13_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox13.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox5.Focus()
        End If
    End Sub

    Private Sub TextBox17_TextChanged(sender As Object, e As EventArgs) Handles TextBox17.TextChanged
        'conn = GetConnect()
        'conn.Open()
        'SQL = "select Name from Supplier where S_Code ='" + TextBox17.Text + "'"
        'cmd = New SqlCommand(SQL, conn)
        'dr = cmd.ExecuteReader
        'While (dr.Read())
        '    Label19.Text = dr.GetValue(0).ToString
        'End While
        'dr.Close()
        'conn.Close()
    End Sub

    Private Sub TextBox12_TextAlignChanged(sender As Object, e As EventArgs) Handles TextBox12.TextAlignChanged

    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Supplier where S_Code ='" + TextBox17.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label19.Text = dr.GetValue(0).ToString
        End While
        dr.Close()
        conn.Close()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        conn = GetConnect()
        conn.Open()
        SQL = "delete from item  where Name = '" + TextBox1.Text + "' and Code = '" + TextBox2.Text + "' "
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Successfully Updated !")
        clr()
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox4.Focus()
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox7.Focus()
        End If
    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox8.Focus()
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Form9.Show()
    End Sub
End Class