# 🕹️ Procedural Dungeon Generator

Repositório dedicado ao estudo de algoritmos de geração procedural para jogos.

## 🧪 01. Cavernas Orgânicas (Autômatos Celulares)

Este experimento utiliza **Autômatos Celulares** para transformar ruído aleatório em estruturas de cavernas orgânicas.

### 🧠 Lógistica do Algoritmo

1.  **Ruído Inicial**: Preenchimento aleatório da matriz baseado em uma porcentagem de preenchimento (`percentGeneration`). 🎲
2.  **Vizinhança**: Para cada célula, checamos as 8 células adjacentes. Se o número de paredes vizinhas for maior que o limiar (`wallAdjacent`), a célula vira parede. 🏠
3.  **Refino**: O processo é repetido em ciclos (`cicleVerification`). Usamos **Corrotinas** para visualizar a evolução do mapa segundo a segundo. 🧹



https://github.com/user-attachments/assets/a50fae3e-3163-480f-8bb8-5884413eb398

