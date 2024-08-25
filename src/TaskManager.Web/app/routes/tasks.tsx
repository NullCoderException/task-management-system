import { json, LoaderFunction } from "@remix-run/node";
import { Outlet, useLoaderData } from "@remix-run/react";
import type { SerializeFrom } from "@remix-run/node";
import TaskList from "~/components/TaskList";
import { Task } from "~/types/task";

const getTasks = async (): Promise<Task[]> => {
  // Mock data for now
  return [
    { id: "1", title: "Task 1", description: "Description 1", status: "todo" },
    {
      id: "2",
      title: "Task 2",
      description: "Description 2",
      status: "in-progress",
    },
  ];
};

export const loader: LoaderFunction = async () => {
  const tasks = await getTasks();
  return json({ tasks });
};

export default function Tasks() {
  const { tasks } = useLoaderData<SerializeFrom<typeof loader>>();

  return (
    <div className="tasks-layout">
      <h1 className="text-2xl font-bold mb-4">Task Manager</h1>
      <div className="max-w-4xl mx-auto">
        <TaskList tasks={tasks} />
      </div>
      <Outlet />
    </div>
  );
}
