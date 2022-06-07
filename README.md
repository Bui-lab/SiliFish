# SiliFish

Developed by Emine Topcu @ Bui Lab, University of Ottawa, 2022

*in **Sili**co* **Fish** is an open-source software to model spinal control of motor movement in larval zebrafish. It is based on the code written by (Roussel *et al.* 2021), which is available on https://github.com/Bui-lab/Code. The 'Single Coil,' 'Double Coil,' and 'Beat and Glide' models representing different swimming behaviours of larval zebrafish are replicated within SiliFish. In addition, the software further allows custom model generation.



## Custom Model Components
The custom model allows you to define cell pools, connections between cell pools and stimuli.

![](Snapshots\CustomComponents.png)



### Cell Pools

A cell pool consists of a specific number of cells. The cells can be spatially laid out in many ways by configuring the x, y, and z coordinates. Similarly, their intrinsic properties can be defined as distributions (Gaussian, uniform, etc.) rather than unique numbers. A timeline can also be defined to limit the active times of the cells in a cell pool.

![](Snapshots\CellPool.png)![](Snapshots\CellpoolDynamic.png)

Within the current version of SiliFish, cells can be neurons or muscle cells. Neurons are modelled as Izhikevich Simple Cells (Dynamical Systems in Neuroscience, p. 272). Muscle cells follow a basic RC model.



### Connections

It is possible to define gap junctions or chemical synapses between two neurons or neuromuscular junctions between neurons and muscle cells. The connections are defined as probabilistic projections from one cell pool to another.

![](Snapshots\Connection.png)



### Stimuli

External stimuli applied to a specific cell pool can be defined.

![](Snapshots\Stimulus.png)



### 2D and 3D Models

It is possible to check the accuracy of the model generated visually by the 2D and 3D outputs of the generated model.

![](Snapshots\3DModel.png)



### Plots

SiliFish has the capability to generate membrane potential, current, and stimulus plots for a single cell or group of cells.

![](Snapshots\Plots.png)




### Disclaimer
The software is made available without any warranties. It is still under development. If you encounter any problems using the software or would like to suggest a new feature, please create a new issue through the [Issues](https://github.com/Bui-lab/SiliFish/issues) link.

