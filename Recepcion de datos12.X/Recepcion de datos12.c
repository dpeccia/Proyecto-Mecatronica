#include <18F4550.h>
#include <stdlib.h>
#fuses HSPLL,NOWDT,NOPROTECT,NOLVP,NODEBUG,PLL5,CPUDIV1,VREGEN,MCLR,USBDIV,  // 48 MHz  para  el  USB y 48 MHz para  el resto del sistema
#use delay(clock=48000000)
#use rs232(baud=9600, xmit=pin_c6, rcv=pin_c7, bits=8, parity=N,stream=standard) 
#use i2c(Master,sda=PIN_B4, scl=PIN_B5, force_sw,fast)
#include <i2c_Flex_LCD.c>   
//#include <LCD420-FLEX.c>

#define sentidox PIN_D0
#define sentidoy PIN_D1
#define sentidoz PIN_D2
#define motorx PIN_D3
#define motory PIN_D4
#define motorz PIN_D5
#define cerox PIN_D6
#define ceroy PIN_D7
#define ceroz PIN_C0
#define profunz PIN_C1
#define dremel PIN_E0
#define enter PIN_E1
#define habilx PIN_E2
#define habily PIN_A0
#define habilz PIN_A1
#define canalb PIN_A2
#define buzzer PIN_A3

char ch;
char mech[5];
char datox[7];
char datoy[7];
int i, j, h, flagfin, salir;
int contador, contador1;
signed int32 x, y, xini, yini, pasosx, pasosy;
float broca, pasosxflot, pasosyflot;
int flagx, flagy, flagxy, flagcomienzo, flagmecha, flagboton;
short int flagpaquete, flagajuste;
signed int32 pasosz, zini, z, alturamecha, guarda;

void moverz(void) 
{
    output_low(habilz);
    pasosz = z - zini;
    
    while (pasosz > 0) 
    {
        output_high(sentidoz); //para abajo
        output_high(motorz);
        delay_us(1200);
        output_low(motorz);
        delay_us(1200);
        pasosz = pasosz - 1;
    }
    while (pasosz < 0) 
    {
        output_low(sentidoz); //para arriba
        output_high(motorz);
        delay_us(1200);
        output_low(motorz);
        delay_us(1200);
        pasosz = pasosz + 1;
    }
    zini = z;
}

void bajarz(void) 
{
    while (pasosz > 0) //rutina que baja el cabezal
    { 
        output_low(habilz);
        pasosz = pasosz - 1;
        output_high(sentidoz);
        output_toggle(motorz);
        delay_us(1200);
    }
}

void subirz(void) 
{
    while (pasosz > 0) //rutina que sube el cabezal
    { 
        output_low(sentidoz);
        pasosz = pasosz - 1;
        output_toggle(motorz);
        delay_us(1200);
    }
}

void conversionpasos(void) 
{
    pasosxflot = (x - xini) / 200.0;
    pasosyflot = (y - yini) / 200.0;
    if (pasosxflot >= 0)
        pasosx = (signed long int) (pasosxflot + 0.5);
    else
        pasosx = (signed long int) (pasosxflot - 0.5);

    if (pasosyflot >= 0)
        pasosy = (signed long int) (pasosyflot + 0.5);
    else
        pasosy = (signed long int) (pasosyflot - 0.5);

    pasosx = pasosx * 2;
    pasosy = pasosy * 2;
}

void perforacion(void) 
{
    output_high(dremel);
    while (flagpaquete == 0);
    while (flagpaquete == 1) 
    {
        while (flagxy == 0);
        while (flagxy == 1) 
        {
            x = (signed long long int) atof(datox);
            y = (signed long long int) atof(datoy);
            conversionpasos();
            xini = x;
            yini = y;
            clear_interrupt(INT_TIMER0);
            set_timer0(230);
            enable_interrupts(INT_TIMER0); //Habilita interrupción timer0
            flagx = 1;
            flagy = 1;
            while (flagxy == 1);
            z = zini + 400; // Baja 4 mm
            moverz();
            z = zini - 400; //sube 4mm
            moverz();
            putc('*');
        }
    }
    output_low(dremel);
}

void moverxy(void) 
{
    clear_interrupt(INT_TIMER0);
    set_timer0(230);
    enable_interrupts(INT_TIMER0); //Habilita interrupción timer0
    flagx = 1;
    flagy = 1;
    flagxy = 1;
    while (flagxy == 1);
}

void posicionmecha(void) 
{
    x = +175000;
    y = +205000;
    conversionpasos();
    xini = x;
    yini = y;
    // pasosx=+1500;     //pasos para ir a punto de prueba
    //pasosy=+1000;     //pasos para ir a puto de prueba
    contador = 2;
    moverxy();
    putc('M');
}

void profundidadmecha(void) 
{
    printf(LCD_PUTC, "\f\1%s", "Ajustando Z");
    printf(LCD_PUTC, "\2%s", "Profundidad de mecha");
    output_low(habilz);
    output_low(habilx);
    output_low(habily);
    output_low(motorz);
    while (input(profunz) == 1) {
        output_high(sentidoz);
        output_high(motorz);
        delay_us(1200);
        output_low(motorz);
        delay_us(1200);
        alturamecha = alturamecha + 1;
    }
    // alturamecha=alturamecha-500; //le resta el espesor de la base + 1mm total 4,5 mm
    zini = alturamecha;
}

void ajusteceroz(void) 
{
    //printf(LCD_PUTC, "\f\1%s", "Ajustando eje Z");
    //printf(LCD_PUTC, "\2%s", "Espere por favor");
    output_low(habilz);
    printf(LCD_PUTC, "\f\1%s", "Buscando cero Z");
    while ((input(ceroz)) == 0) {
        output_low(sentidoz);
        output_toggle(motorz);
        delay_us(1200);
    }
    printf(LCD_PUTC, "\f\1%s", "Buscando cero Z OK");
    zini = 0;
}

void ajusteceroxy(void) 
{
    printf(LCD_PUTC, "\f\1%s", "Ajustando ejes X-Y");
    printf(LCD_PUTC, "\2%s", "Espere por favor");
    //ajusteceroz();
    output_low(habilx);
    output_low(habily);
    while ((input(cerox) == 0) || (input(ceroy)) == 0) 
    {
        if (input(cerox) == 0) 
        {
            output_high(sentidox);
            output_toggle(motorx);
        }
        if (input(ceroy) == 0) 
        {
            output_low(sentidoy);
            output_toggle(motory);
        }
        delay_us(1200);
    }
    output_low(motorx);
    output_low(motory);
    output_low(motorz);
    xini = 0;
    yini = 0;
}

void mecha(void) 
{
    while (flagmecha == 0);
    while (flagmecha == 1);
    broca = atof(mech);
}

void bip(void) 
{
    output_high(buzzer);
    contador1 = 5; // 1/48*4*65586*8*23=218453useg aprox
    clear_interrupt(INT_TIMER1); //limpio la bandera
    set_timer1(0);
    enable_interrupts(INT_TIMER1);
}

#INT_EXT2         //Atención a interrupción por cambio en RB2

ext_isr1() //Función de interrupción
{ 
    putc('F');
    flagajuste = 0;
    salir = 1;
    // disable_interrupts(int_ext2_L2H);
}

#INT_EXT         //Atención a interrupción por cambio en RB0

void INTEXT_isr(void) //Función de interrupción
{ 
    output_low(motorz);
    if (input(canalb) == 0) 
    {
        output_low(habilz);
        output_low(sentidoz); //mecha para arriba
        z = z - 1;
        output_high(motorz);
        delay_us(1200);
        output_low(motorz);
        delay_us(1200);
    }
    if (input(canalb) == 1) 
    {
        output_low(habilz);
        output_high(sentidoz); //mecha para abajo
        z = z + 1;
        output_high(motorz);
        delay_us(1200);
        output_low(motorz);
        delay_us(1200);
    }
}
#int_TIMER0

void TIMER0_isr(void) 
{
    contador = contador - 1;

    if (contador == 0) 
    {
        contador = 2;
        if (pasosx == 0) 
        {
            flagx = 0;
        }
        if (pasosx > 0) 
        {
            output_low(sentidox);
            output_toggle(motorx);
            pasosx = pasosx - 1;
        }
        if (pasosx < 0) 
        {
            output_high(sentidox);
            output_toggle(motorx);
            pasosx = pasosx + 1;
        }

        if (pasosy == 0) 
        {
            flagy = 0;
        }
        if (pasosy > 0) 
        {
            output_high(sentidoy);
            output_toggle(motory);
            pasosy = pasosy - 1;
        }
        if (pasosy < 0) 
        {
            output_low(sentidoy);
            output_toggle(motory);
            pasosy = pasosy + 1;
        }

        if (flagx == 0 && flagy == 0) 
        {
            flagxy = 0;
            disable_interrupts(INT_TIMER0); //deshabilita interrupción timer0
        }

    }
    set_timer0(230); //Se recarga el timer0
}

#int_TIMER1

void TIMER1_isr(void) // Timer para buzzer
{
    contador1 = contador1 - 1;

    if (contador1 == 0) 
    {
        output_low(buzzer);
        disable_interrupts(INT_TIMER1);
    }
    set_timer1(0);
}
#int_rda

void serial_isr() 
{
    ch = getchar();
    if (ch == 'F' && flagpaquete == 1) //Ciclo terminado
    {
        flagpaquete = flagpaquete + 1;
        flagfin = 1;
    }
    if (ch == 'A') //Inicio Ajuste
    {
        flagajuste = flagajuste + 1;
    }
    if (ch == 'P') //Dato de perforacion entrante
    {
        flagpaquete = flagpaquete + 1;
    }
    if (ch == 'S') 
    {
        flagcomienzo = 1;
    }
    if (ch == 'M') //Mecha entrante
    {
        flagmecha = 1;
        h = 0;
    }
    if ((flagmecha) == 1 && (ch != 'M')) //Dato mecha
    {
        mech[h] = ch;
        h = h + 1;
        if (h == 5) 
        {
            flagmecha = 0;
        }
    }
    if (ch == 'X') //Valor X entrante
    {
        flagx = 1;
        i = 0;
    }
    if (ch == 'Y') //Valor Y entrante
    {
        flagy = 1;
        j = 0;
    }
    if ((flagx) == 1 && (ch != 'X')) //Dato X
    {
        datox[i] = ch;
        i = i + 1;
        if (i == 7) 
        {
            flagx = 0;
            putc('*');
        }
    }
    if ((flagy) == 1 && (ch != 'Y')) //Dato Y
    {
        datoy[j] = ch;
        j = j + 1;
        if (j == 7) {
            flagy = 0;
            flagxy = 1;
            putc('*');
        }
    }
}

void main() 
{
    delay_ms(1000);
    enable_interrupts(INT_RDA);
    setup_timer_0(RTCC_8_BIT | RTCC_DIV_256); //Configuración timer0
    setup_timer_1(T1_INTERNAL | T1_DIV_BY_8); //Configuración timer1

    enable_interrupts(INT_EXT2_L2H); //Habilita int. RB2?
    //ext_int_edge(L_TO_H);              //por flanco de subida
    enable_interrupts(GLOBAL); //Habilita interrupción general
    lcd_init();

    while (1) {
        flagfin = 0;
        flagmecha = 0;
        flagcomienzo = 0;
        xini = 0;
        yini = 0;
        zini = 0;
        flagajuste = 0;
        flagpaquete = 0;
        pasosx = 0;
        pasosz = 0;
        pasosy = 0;
        alturamecha = 0;
        x = 0;
        y = 0;
        z = 0;
        flagboton = 0;
        flagxy = 0;
        flagx = 0;
        flagy = 0;
        broca = 0;
        pasosxflot = 0;
        pasosyflot = 0;
        salir = 0;
        i = 0;
        j = 0;
        h = 0;
        output_high(habilx); //motores sin energía
        output_high(habily);
        output_high(habilz);
        output_low(dremel); //apago el dremel
        output_low(buzzer); // apago buzzer
        lcd_putc("\fHAGA CLICK EN");
        lcd_putc("\2CALIBRAR EN LA");
        lcd_putc("\3COMPUTADORA Y ESPERE");
        lcd_putc("\4UN MOMENTO.");
        while (flagcomienzo == 0);
        flagcomienzo = 0;
        ajusteceroz();
        ajusteceroxy();
        posicionmecha();
        mecha();
        printf(lcd_putc"\f\1Coloque mecha %01.2fmm", broca);
        lcd_putc("\2Presione enter para");
        lcd_putc("\3comenzar el ajuste");
        lcd_putc("\4de la placa.");
        while (input(enter) == 0) 
        {
        }
        delay_ms(10);
        bip();
        while (input(enter) == 1) 
        {
        }
        alturamecha = 0;
        profundidadmecha();
        zini = alturamecha;
        z = alturamecha - 500; 
        moverz();
        ajusteceroxy();
        putc('A');
        while (flagajuste == 0 && salir == 0);

        while (flagajuste == 1) 
        {
            while (flagxy == 0);
            while (flagxy == 1) 
            {
                x = (signed long long int) atof(datox);
                y = (signed long long int) atof(datoy);
                conversionpasos();
                xini = x;
                yini = y;
                clear_interrupt(INT_TIMER0);
                set_timer0(230);
                enable_interrupts(INT_TIMER0); //Habilita interrupción timer0
                flagx = 1;
                flagy = 1;
                while (flagxy == 1);
                lcd_putc("\f\1Gire para subir o"); 
                lcd_putc("\2bajar mecha.Presione");
                lcd_putc("\3enter nuevo punto.");
                lcd_putc("\4Boton rojo salir.");
                clear_interrupt(INT_EXT_L2H);
                enable_interrupts(INT_EXT_L2H);
                while (input(enter) == 0 && salir == 0) 
                {
                }
                delay_ms(10);
                bip();
                while (input(enter) == 1 && salir == 0) 
                {
                }
                if (input(enter) == 0 && salir == 0) 
                {
                    guarda = zini;
                    zini = z;
                    z = guarda;
                    moverz();
                    putc('A');
                }
            }
        }
        bip();
        disable_interrupts(INT_EXT_L2H);
        //salir=0;
        while (flagfin == 0) {

            if (salir == 0) {
                ajusteceroz();
                ajusteceroxy();
                posicionmecha();
                mecha();
                lcd_putc("\f\1Por favor coloque");
                printf(lcd_putc"\2mecha de %01.2fmm y", broca);
                lcd_putc("\3presione enter");


                while (input(enter) == 0) 
                {
                }
                delay_ms(10);
                bip();
                while (input(enter) == 1) 
                {
                }
                alturamecha = 0;
                profundidadmecha();
                zini = alturamecha;
                z = alturamecha - 500;
                moverz();
                ajusteceroxy();
                puts("OK");
            }
            ajusteceroxy();
            contador = 2;
            lcd_putc("\f\1Perforadora Lista");
            lcd_putc("\2para operar haga click");
            lcd_putc("\3en \"Comenzar\"");
            while (flagcomienzo == 0);
            
            lcd_putc("\f\1PERFORANDO");
            flagcomienzo = 0;
            perforacion();
            salir = 0;
        }
    }
}