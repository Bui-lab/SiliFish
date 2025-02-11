# pylint: disable=C0301, C0303, C0114, C0103, C0116
from os import listdir
import numpy as np
import pandas as pd


def add_group_column(file, group_label):
    data = pd.read_csv(file)
    data['Group'] = group_label
    return data

def create_tail_dataframe(file_list, group, column, episode = 0):
    if episode==0: # read the full content
        df = pd.concat([add_group_column(file, group) for file in file_list])
        df[column] = pd.to_numeric(df[column], errors='coerce')
        df = df.dropna(subset=[column])
    else:
        df = None
        for file in file_list:
            sub_df = add_group_column(file, group)
            idx = np.where(sub_df[column].apply(lambda x: str(x).startswith('Na')))
            if len(idx[0]) < episode: 
                raise Exception("Wrong csv format") 
            start = idx[0][episode - 1]
            if len(idx[0]) > episode: 
                end = idx[0][episode]
                sub_df = sub_df.iloc[start+1:end]
            else:
                sub_df = sub_df.iloc[start+1:]
            if df is None:
                df = sub_df
            else:
                df = pd.concat([df, sub_df])
        df[column] = pd.to_numeric(df[column], errors='coerce')
        df = df.dropna(subset=[column])
    return df

def read_tail_files(folder):
    ten_freq_files = []
    ten_move_files = []
    for filename in listdir(folder):
        if filename.endswith("Tail Beat Frequency.csv"):
            ten_freq_files.append(folder+"/"+filename)
        elif filename.endswith("Tail Movement.csv"):
            ten_move_files.append(folder+"/"+filename)
    return ten_freq_files, ten_move_files

    
