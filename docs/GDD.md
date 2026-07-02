# 🎮 Game Design Document (GDD)
## Animal Rescue: Fred Costa

---

## 1. Visão Geral do Jogo

### 1.1 Conceito
**Animal Rescue: Fred Costa** é um jogo de ação e aventura em 3D onde o jogador controla Fred Costa, um herói dedicado ao resgate de animais feridos e maltratados. O objetivo principal é completar missões de resgate, superando obstáculos ambientais e inimigos, para salvar cães e gatos que sofrem abandono.

### 1.2 Género
- Action-Adventure
- Puzzle Solving
- Platformer Elements

### 1.3 Plataformas Alvo
- Mobile (iOS/Android)
- Web (WebGL)

### 1.4 Público-Alvo
- Idade: 6-16 anos
- Jogadores casuais e hardcore
- Amantes de animais
- Conscientização sobre proteção animal

---

## 2. Mecânicas Principais

### 2.1 Movimento e Controle
- **Movimento 3D**: O jogador controla Fred Costa com joystick virtual (mobile) ou WASD (web)
- **Câmera**: Câmera dinâmica que segue o personagem
- **Interação**: Botão de ação para resgatar animais e usar itens

### 2.2 Sistema de Resgate
- Quando Fred se aproxima de um animal ferido:
  - Exibe UI de "Pressione para Resgatar"
  - Se o animal está muito ferido, é necessário usar itens de cura primeiro
  - Ao resgatar: Animal é salvo e adicionado ao "Contador de Resgate"
  - Ganho de pontos baseado na saúde do animal

### 2.3 Sistema de Saúde
- **Fred Costa**:
  - Barra de saúde
  - Pode levar dano de obstáculos e inimigos
  - Pode se curar com itens
  - Game Over ao atingir 0 HP

- **Animais**:
  - Cada animal tem uma saúde inicial
  - Podem estar feridos (visualmente indicado)
  - Precisam de itens de cura antes do resgate completo

### 2.4 Sistema de Inventário
- Itens disponíveis:
  - **Bandagens**: Curam pequenos ferimentos
  - **Poções de Cura**: Curam muitos ferimentos
  - **Ração**: Alimenta animais medo/agressivos
  - **Chaves**: Destravam portas
  - **Escadas Portáteis**: Alcançam áreas altas

### 2.5 Obstáculos
- **Ambientais**:
  - Picos/Espinhos
  - Fossas
  - Plataformas móveis
  - Áreas com fogo
  - Portas travadas

- **Inimigos**:
  - Seguranças malvados
  - Robôs patrulha
  - Animais agressivos (opcionalmente)

### 2.6 Sistema de Progressão
- **Níveis**: 3+ níveis com dificuldade crescente
- **Pontos**: Baseados em:
  - Animais resgatados
  - Saúde final do animal
  - Velocidade de conclusão
  - Danos evitados

- **Estrelas**: Sistema de 3 estrelas por nível
  - ⭐⭐⭐: Missão perfeita (sem danos, animais com saúde máxima)
  - ⭐⭐: Boa execução
  - ⭐: Missão completada

---

## 3. Estrutura de Níveis

### Nível 1: O Abrigo Abandonado
- **Localização**: Um abrigo antigo perto da cidade
- **Objetivos**: Resgatar 3 cães
- **Dificuldade**: Fácil
- **Novos Obstáculos**: Picos, fossas simples
- **Novos Itens**: Bandagens

### Nível 2: A Fábrica Desativada
- **Localização**: Área industrial
- **Objetivos**: Resgatar 2 cães e 1 gato
- **Dificuldade**: Médio
- **Novos Obstáculos**: Plataformas móveis, inimigos (seguranças)
- **Novos Itens**: Poções de cura, chaves

### Nível 3: O Laboratório Secreto
- **Localização**: Instalação subterrânea
- **Objetivos**: Resgatar 3 gatos
- **Dificuldade**: Difícil
- **Novos Obstáculos**: Áreas com fogo, robôs patrulha
- **Novos Itens**: Escadas portáteis, todas as poções

---

## 4. Arte e Estilo Visual

### 4.1 Estilo Visual
- **3D Moderno com toques Cartoon**
- Cores vibrantes e atrativas
- Personagens expressivos
- Mundo imersivo mas acessível

### 4.2 Personagens
- **Fred Costa**: Herói corajoso, design heroico
- **Cães**: Diferentes raças, todos com expressões tristes iniciais
- **Gatos**: Diferentes cores, corpo encolhido/assustado
- **Inimigos**: Design "malvado" mas não assustador

### 4.3 Cenários
- Abrigo: Desgastado, triste
- Fábrica: Industrial, metal e concreto
- Laboratório: Futurista, neon

---

## 5. Áudio

### 5.1 Música
- **Menu**: Tema inspirador
- **Gameplay**: Músicas dinâmicas que mudam com tensão
- **Vitória**: Tema festivo
- **Derrota**: Tema triste

### 5.2 Efeitos Sonoros
- Resgate de animal: Som satisfatório
- Dano recebido: Som alertador
- Cura: Som reconfortante
- Obstáculo: Sons variados

### 5.3 Vozes
- Fred Costa: Vozes de esforço/celebração
- Animais: Sons realistas (latidos, miados, ganidos)

---

## 6. Fluxo de Jogo

```
┌─────────────┐
│ Menu Principal │
└────┬────────┘
     │
     ├─→ Jogar
     │   └─→ Seleção de Nível
     │       └─→ Carregando Nível
     │           └─→ Gameplay
     │               ├─→ Vitória → Tela de Resultado
     │               └─→ Derrota → Game Over
     │
     ├─→ Configurações
     │   └─→ Volume, Dificuldade, etc
     │
     └─→ Sair
```

---

## 7. Sistema de Dificuldade

### Fácil
- Mais itens disponíveis
- Inimigos mais lentos
- Dano reduzido
- Animais com mais saúde

### Normal
- Equilíbrio padrão
- Inimigos normais
- Dano normal

### Difícil
- Menos itens
- Inimigos mais rápidos e inteligentes
- Dano aumentado
- Animais mais feridos
- Contra-tempo mais curto

---

## 8. Próximas Fases (Futuro)
- Modo multiplayer local
- Mais níveis (4-10)
- Personagens desbloqueáveis
- Mini-games de cura
- Sistema de ligação emocional com animais
- Histórias de fundo para cada animal

---

**Versão**: 1.0  
**Data de Atualização**: Julho 2026  
**Status**: Documento Base Completo