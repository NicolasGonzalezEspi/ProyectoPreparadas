# 🎯 Gestión Multiplataforma de Usuarios y Pedidos con Supervisión Inteligente

**Ciclo Formativo:** Desarrollo de Aplicaciones Multiplataforma (DAM)  
**Autor:** Nicolás González Espinosa
**Curso:** 2024 - 2025

---

## 📚 Índice

- [📘 Introducción](#-introducción)
- [🛠️ Funcionalidades del proyecto y tecnologías utilizadas](#️-funcionalidades-del-proyecto-y-tecnologías-utilizadas)
- [⚙️ Guía de instalación](#️-guía-de-instalación)
- [📎 Guía de uso](#-guía-de-uso)
- [📄 Enlace a la documentación](#-enlace-a-la-documentación)
- [🎨 Enlace a Figma de la interfaz](#-enlace-a-figma-de-la-interfaz)
- [✅ Conclusión](#-conclusión)
- [🤝 Contribuciones, agradecimientos y referencias](#-contribuciones-agradecimientos-y-referencias)
- [📄 Licencias](#-licencias)
- [📬 Contacto](#-contacto)

---

## 📘 Introducción

Este proyecto consiste en una aplicación de escritorio profesional desarrollada en **.NET WPF**, con conexión a base de datos **MySQL remota mediante SSH** (en su versión Empresarial, en esta versión funciona con una MySQL local. Está diseñada para facilitar el control, gestión y supervisión de flujos de datos provenientes de múltiples plataformas, en especial usuarios y pedidos.

### 🎯 Justificación
La gestión de datos de múltiples plataformas suele generar redundancia, errores e ineficiencia. Este proyecto soluciona ese problema con una arquitectura moderna, fiable y visualmente clara.

### 🧭 Objetivos

- Unificar, depurar y exportar usuarios de distintas plataformas.
- Gestionar pedidos complejos incluyendo incidencias, productos y trazabilidad.
- Supervisar acciones de usuarios con logs, gráficos y reportes.
- Automatizar decisiones mediante el sistema de “siguiente paso”.

### 💡 Motivación

Este proyecto nace del reto real de trabajar con múltiples plataformas, sistemas y flujos de datos que debían ser optimizados.

---

## 🛠️ Funcionalidades del proyecto y tecnologías utilizadas

### 🧩 Módulos principales

#### 1. Gestión de usuarios
- Importación de usuarios desde CSV.
- Fusión, depuración y exportación.
- Almacenamiento histórico a largo plazo.

#### 2. Gestión de pedidos
- Importación/exportación de pedidos.
- Seguimiento de tracking, SN, abonos, facturas...
- Integración con productos y sistema de incidencias por trigger.

#### 3. Supervisión
- Gráficos (barras, líneas, sectores).
- Sistema de logs con hora y fecha.
- Recomendación automatizada del “siguiente paso”.
- Generación de reportes PDF históricos.

---

### ⚙️ Tecnologías utilizadas

- **Frontend:** .NET WPF, XAML
- **Backend:** C# (.NET Framework)
- **Base de Datos:** MySQL
- **Exportación/Importación:** CSV, Excel
- **Estética:** WPF Resource Dictionaries + diseño profesional
- **Gráficos:** LiveCharts 
- **Reportes:** Generación automática de PDFs

---

# 🧠 Programa de Gestión – Preparadas

**El programa que agiliza y automatiza procesos en Preparadas.**

---

## 📦 Guía de Instalación

1. Descargar el archivo ZIP: `Preparadas Gestión`.
2. Descomprimir (preferiblemente fuera de la carpeta de Descargas).
3. Ejecutar el archivo `ProgramaPreparadasGestion.exe` (icono verde con una "A" y flechas).
4. La primera vez que lo abras, aparecerá una ventana indicando que necesitas instalar un componente adicional.
   - Haz clic en **Download it now**.
   - Se abrirá una ventana oficial de Microsoft para descargar `windowsdesktop-runtime`.
   - Si no se descarga automáticamente, haz clic en **haz clic aquí para descargar manualmente**.
5. Instala el componente haciendo clic en el instalador.
6. Una vez instalado, vuelve a ejecutar `ProgramaPreparadasGestion.exe`.

✅ ¡Listo! Ya puedes utilizar la aplicación.

---

## 🧰 Guía de Uso

La aplicación cuenta con varios módulos, cada uno con funcionalidades específicas para la gestión de datos y pedidos.

### 🎓 Gestión de Puntos

#### 🆕 Nuevas Convocatorias

- Importa datos de alumnas que han completado un curso (CSV desde la plataforma Preparadas).
- Aparecen en una tabla editable donde se pueden actualizar campos como "Tablet" o "Puntos".
- Una vez revisados, pulsa **2. Pasar a Formato MKP** para transferir los datos al siguiente paso.

#### 🧮 Formato Marketplace (MKP)

1. **Importar Saldos MKP**: Importa el estado actual del Marketplace desde WooCommerce.
2. **Totalizar Saldos**: Suma puntos duplicados por DNI y elimina registros con 0 puntos.

> Ejemplo:
> ```
> Fernando Álvarez, DNI 12345678P, 40 puntos
> Fernando Álvarez, DNI 12345678P, 40 puntos
> ```
> Tras totalizar:
> ```
> Fernando Álvarez, DNI 12345678P, 80 puntos
> ```

#### 📤 Importar MKP

- Exporta los datos preparados en un nuevo archivo CSV para su importación en el Marketplace.
- Finaliza pulsando **Mover Nuevas Convos a BBDDGlobal**, lo que guarda los datos para histórico y backup.

---

### 📦 Gestión de Pedidos

#### 📥 Pedidos

- **Importar nuevos Pedidos** desde WooCommerce.
  - Ve a WooCommerce > Pedidos > Seleccionar > Exportar como CSV.
  - Importa ese CSV en la aplicación.

- **Vista Optimizada**: permite personalizar la visualización de datos.
- **Buscar Por**: busca en campos como ID, nombre o dirección.
- **Filtrar Por**: filtra por estados o proveedores.
- **Cambiar Estado**: modifica el estado de múltiples pedidos a la vez.

#### ⚠️ Incidencias

- Muestra problemas con pedidos.
- Puedes añadir/modificar información de seguimiento o resolución.

#### 🛒 Productos

- Visualiza y crea nuevos productos para el Marketplace.

---

### 🔍 Consulta de Datos

- Consulta históricos de Nuevas Convocatorias y datos procesados.
- Sistema de importación/exportación de backups en formato CSV.

---

## 🧭 Navegación

> La aplicación se navega desde la **barra lateral izquierda**.  
> Botones **grises**: entrada/salida de archivos.  
> Botones **azules**: operaciones internas de la App.
> Usa el Sistema Izquierda, Derecha. Arriba, Abajo. Además, en aquellas operaciones del DTL hay índices: 1A, 2B...

---

## ✅ Conclusión

Esta aplicación facilita la gestión de procesos complejos en Preparadas, automatizando tareas que antes se realizaban manualmente y reduciendo tiempos operativos.  
Gracias a su estructura modular, interfaz clara y operaciones inteligentes, mejora drásticamente la eficiencia y trazabilidad de datos.

---

## 🤝 Contribuciones, Agradecimientos y Referencias

- **Desarrollador Principal**: [Tu nombre]
- **Frameworks usados**: .NET WPF, MySQL, SSH
- **CRM Integrado**: WooCommerce (WordPress)
- **Inspiración y Casos de Uso**: Equipo de gestión de Preparadas

---

## 📄 Licencia

Este proyecto se publica bajo la licencia MIT. Puedes usarlo, modificarlo y distribuirlo libremente.  
Consulta el archivo `LICENSE` para más detalles.

---

## 📬 Contacto

- ✉️ Email: [nicolascmyr@gmail.com]

---


