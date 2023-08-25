# Se generan los imports, los mismos que el segregation model
from mesa import Agent, Model 

# Debido a que necesitamos que existe un solo agente por celda, elegimos ''SingleGrid''.
from mesa.space import SingleGrid

# Con ''Simultaneousctivation'', hacemos que todos los agentes se activen ''al mismo tiempo''.
from mesa.time import RandomActivation

# Haremos uso de ''DataCollector'' para obtener información de cada paso de la simulación.
from mesa.datacollection import DataCollector

# matplotlib lo usaremos crear una animación de cada uno de los pasos del modelo.
import matplotlib
import matplotlib.pyplot as plt
import matplotlib.animation as animation
plt.rcParams["animation.html"] = "jshtml"
matplotlib.rcParams['animation.embed_limit'] = 2**128

# Importamos los siguientes paquetes para el mejor manejo de valores numéricos.
import numpy as np
import pandas as pd

# Definimos otros paquetes que vamos a usar para medir el tiempo de ejecución de nuestro algoritmo.
import time
import datetime

# Voy a tratar de rehacer los agentes en este bloque, quiero poder comprenderlo al 100% y rectificar errores, pero no borro el otro agente para no perderlo.
class FishAgent(Agent):
    def __init__(self, current_id, model, energy, fertility = 0):
        super().__init__(current_id, model)
        self.energy = energy
        self.fertility = fertility
        
    def step(self):
        self.advance()
        
    def move(self):
        # Vamos a definir los movimientos posibles para que concuerde con lo del vecindario de Vonn Newmann.
        possible_moves = self.model.grid.get_neighborhood(self.pos, moore=False, include_center=False)
        # Vamos a eliminar de possible moves los espacios utilizados
        empty_cells = [cell for cell in possible_moves if self.model.grid.is_cell_empty(cell)]
        
        # Ahora si, hacemos el movimiento aleatorio.
        if empty_cells:
            new_position = self.random.choice(empty_cells)
            self.model.grid.move_agent(self, new_position)
            
        else:
            return  None
        
    def advance(self):
        self.move()
        self.energy -= 1
        if self.energy <= 0:
            self.model.grid.remove_agent(self)
            self.model.schedule.remove(self)
        else:
            if self.fertility == 4:
                self.reproduce()
            else:
                self.fertility += 1
    
    def reproduce(self, fertility_threshold = 4):
        if self.fertility == fertility_threshold:
            # Recordar la posición actual del agente padre
            current_position = self.pos
            
            empty_cells = self.model.grid.get_neighborhood(self.pos, moore=False, include_center=False)
            empty_cells = [cell for cell in empty_cells if self.model.grid.is_cell_empty(cell)]
            
            if empty_cells:
                new_position = self.random.choice(empty_cells)
                self.model.grid.move_agent(self, new_position)
                energy_of_offspring = 20
                offspring = FishAgent(self.model.next_id(), self.model, energy=energy_of_offspring, fertility=0)
                self.model.grid.place_agent(offspring, current_position)  # Colocar el offspring en la posición anterior del padre
                self.model.schedule.add(offspring)
                
                self.fertility = 0
                
            else:
                return  None
        else:
            return  None

            
class SharkAgent(Agent):
    def __init__(self, current_id, model, energy, fertility = 0):
        super().__init__(current_id, model)
        self.energy = energy
        self.fertility = fertility
        
    def step(self):
        self.advance()
    
    def move(self):
        # Vamos a definir los movimientos posibles para que concuerde con lo del vecindario de Vonn Newmann.
        possible_moves = self.model.grid.get_neighborhood(self.pos, moore=False, include_center=False)
        # Vamos a eliminar de possible moves los espacios utilizados
        empty_cells = [cell for cell in possible_moves if self.model.grid.is_cell_empty(cell)]
        
        # Ahora si, hacemos el movimiento aleatorio.
        if empty_cells:
            new_position = self.random.choice(empty_cells)
            self.model.grid.move_agent(self, new_position)
            
        else:
            return None
            
    def advance(self):
        
        self.energy -= 1
        if self.energy <= 0:
            self.model.grid.remove_agent(self)
            self.model.schedule.remove(self)
        else:
            self.eat()
            self.move()
            if self.fertility == 12:
                self.reproduce()
            else:
                self.fertility += 1
                
    def reproduce(self, fertility_threshold = 12):
        if self.fertility == fertility_threshold:
            # Recordar la posición actual del agente padre
            current_position = self.pos
            
            empty_cells = self.model.grid.get_neighborhood(self.pos, moore=False, include_center=False)
            empty_cells = [cell for cell in empty_cells if self.model.grid.is_cell_empty(cell)]
            
            if empty_cells:
                new_position = self.random.choice(empty_cells)
                self.model.grid.move_agent(self, new_position)
                energy_of_offspring = 3
                offspring = SharkAgent(self.model.next_id(), self.model, energy=energy_of_offspring, fertility=0)
                self.model.grid.place_agent(offspring, current_position)  # Colocar el offspring en la posición anterior del padre
                self.model.schedule.add(offspring)
                
                self.fertility = 0


    
    def eat(self):
        cellmates = [n for  n in self.model.grid.get_neighbors(self.pos, moore = False, include_center = False)]
        fish = [obj for obj in cellmates if isinstance(obj, FishAgent)]
        if len(fish) > 0:
            prey = self.random.choice(fish)
            (x,y) = prey.pos
            self.model.grid.remove_agent(prey)
            self.model.schedule.remove(prey)
            self.model.grid.move_agent(self, (x,y))
            self.energy += 2
        
def get_grid(model):
    grid = np.zeros((model.grid.width, model.grid.height, 3))  # Usamos una matriz de 3 dimensiones para almacenar colores
        
    for cell in model.grid.coord_iter():
        cell_content, (x, y) = cell
        if cell_content is not None:
            cell_agent = cell_content
            if isinstance(cell_agent, FishAgent):
                grid[x][y] = [1, 0, 0]  # Rojo para los peces
            elif isinstance(cell_agent, SharkAgent):
                grid[x][y] = [0, 0, 1]  # Azul para los tiburones
        else:
            grid[x][y] = [1, 1, 1]  # Blanco para los espacios vacíos
                    
    return grid
    
class WaTorModel(Model):
    def __init__(self, width, height, initial_fish, initial_sharks):
        self.num_agents = initial_fish + initial_sharks
        self.grid = SingleGrid(width, height, torus=True)
        self.schedule = RandomActivation(self)  
        self.datacollector = DataCollector(model_reporters={"Grid": get_grid})
        self.initial_fish = initial_fish
        self.initial_sharks = initial_sharks
        self.current_id = 0  
            
        num_fish = initial_fish
        while self.grid.exists_empty_cells() and num_fish > 0:
            x = self.random.randrange(self.grid.width)
            y = self.random.randrange(self.grid.height)
            if self.grid.is_cell_empty((x, y)):
                energy_of_fish = 20
                fertility_of_fish = 0
                fish = FishAgent(self.next_id(), self, energy=energy_of_fish, fertility=fertility_of_fish)
                self.grid.place_agent(fish, (x, y))
                self.schedule.add(fish)
                num_fish -= 1
        num_sharks = initial_sharks 
        while self.grid.exists_empty_cells() and num_sharks > 0:
            x = self.random.randrange(self.grid.width)
            y = self.random.randrange(self.grid.height)
            if self.grid.is_cell_empty((x, y)):
                energy_of_shark = 3
                fertility_of_shark = 0
                shark = SharkAgent(self.next_id(), self, energy=energy_of_shark, fertility=fertility_of_shark)
                self.grid.place_agent(shark, (x, y))
                self.schedule.add(shark)
                num_sharks -= 1
                    
    def step(self):
        self.datacollector.collect(self)
        self.schedule.step()
        
GRID_WIDTH = 75
GRID_HEIGHT = 50
INITIAL_FISH = 500
INITIAL_SHARKS = 40
MAX_GENERATIONS = 100

    # Crear el modelo
model = WaTorModel(GRID_WIDTH, GRID_HEIGHT, INITIAL_FISH, INITIAL_SHARKS)

    # Ejecutar simulación
for i in range(MAX_GENERATIONS):
    model.step()
        
    
all_grid = model.datacollector.get_model_vars_dataframe()
    
colored_grid = get_grid(model)

# Mostrar la grilla usando matplotlib
plt.imshow(colored_grid)

anim = animation.FuncAnimation(plt.gcf(), lambda i: plt.imshow(all_grid.iloc[i][0]), frames=len(all_grid))
anim.save('WaTor.gif', writer='ffmpeg', fps=2)