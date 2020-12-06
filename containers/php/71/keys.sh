#!/bin/bash

cp /ssh/id_rsa /root/.ssh/id_rsa
cp /ssh/id_rsa.pub /root/.ssh/id_rsa.pub
cp /ssh/known_hosts /root/.ssh/known_hosts

chmod 600 /root/.ssh/id_rsa
chmod 600 /root/.ssh/id_rsa.pub
chmod 600 /root/.ssh/known_hosts